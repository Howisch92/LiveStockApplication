using LiveStockApplication.Hubs;
using LiveStockApplication.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace LiveStockApplication.Service
{
    public class StockService : IStockService
    {
        private readonly IHubContext<StockmarketHub> _hub;
        
        private bool marketOpen = false;

        private IList<StockModel> listOfStocks { get; set; }

        public StockService(IHubContext<StockmarketHub> hub) 
        {
            _hub = hub;
        }

        public void OpenStockMarket() 
        {
            if(!marketOpen) { 

                listOfStocks = InitializeStockmarket();
                var fromTimespan = TimeSpan.Zero;
                var updateInterval = TimeSpan.FromSeconds(2);

                var timer = new Timer((e) =>
                {

                    IList<StockViewModel> viewmodelToreturn = new List<StockViewModel>();
                    foreach (var stock in listOfStocks)
                    {
                        Random updateOrNot = new Random();
                        var r = updateOrNot.NextDouble();
                        if (r > 0.2)
                        {
                            CalculateNewStockPricings(stock);
                        }
                       
                        StockViewModel model = new StockViewModel(stock._stockId, stock._symbol, stock._bidPrice, stock._askPrice);
                        viewmodelToreturn.Add(model);
                    }
                    _hub.Clients.All.SendAsync("UpdateStockmarket", viewmodelToreturn);
                }, null, fromTimespan, updateInterval);
                
                marketOpen = true;
            }

        }


        public IList<StockViewModel> GetListOfAllStocks()
        {
            IList<StockViewModel> listOfStockViewModels = new List<StockViewModel>();
            foreach (StockModel stock in this.listOfStocks)
            {
                StockViewModel model = new StockViewModel(stock._stockId, stock._symbol, stock._bidPrice, stock._askPrice);
                listOfStockViewModels.Add(model);
            }
            return listOfStockViewModels;
        }


        private IList<StockModel> InitializeStockmarket()
        {
            List<StockModel> initialStockmarket = new List<StockModel>();
            initialStockmarket.Add(new StockModel(1,"AAPL", 156.20, 156.99, 0.004, null));
            initialStockmarket.Add(new StockModel(2,"IBM", 155.10, 160.85, 0.001, null));
            initialStockmarket.Add(new StockModel(3,"MSFT", 78.00, 78.65, 0.0004, null));
            initialStockmarket.Add(new StockModel(4,"Fætter BR", 250.11, 250.55, 0.15, false));
            initialStockmarket.Add(new StockModel(5,"SpaceX", 10.00, 11.70, 0.131, true));

            return initialStockmarket;
        }
        private void CalculateNewStockPricings(StockModel stock)
        {
            if (stock._bidPrice > 0)
            {
                var random = new Random((int)Math.Floor(stock._bidPrice));
                var change = stock._bidPrice * stock._stockPercentageValue;
                if (stock._stockIncreaseOrDecrease != null)
                {
                    change = (bool)stock._stockIncreaseOrDecrease ? change : -change;
                }
                else
                {
                    var ran = random.NextDouble() > 0.51;
                    change = ran ? change : -change;
                }
                stock._bidPrice = Math.Round(stock._bidPrice + change, 2);

                stock._askPrice = Math.Round((stock._askPrice + change + (change * 0.00001)), 2);
            }
            else
            {
                stock._bidPrice = 0;
            }

        }

    }
}

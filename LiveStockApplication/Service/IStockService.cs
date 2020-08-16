using LiveStockApplication.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace LiveStockApplication.Service
{
    public interface IStockService
    {
        void OpenStockMarket();
        IList<StockViewModel> GetListOfAllStocks();
    }
}
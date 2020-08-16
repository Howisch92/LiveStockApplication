using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveStockApplication.Models
{
    public class StockModel
    {
        public int _stockId { get; set; }
        public string _symbol { get; set; }
        public double _askPrice { get; set; }
        public double _bidPrice { get; set; }
        public double _stockPercentageValue { get; set; }
        public bool? _stockIncreaseOrDecrease { get; set; }

        public StockModel(int stockId ,string symbol, double bidPrice, double askPrice, double stockPercentageValue,  bool? stockIncreaseOrDecrease)
        {
            _stockId = stockId;
            _symbol = symbol;
            _askPrice = askPrice;
            _bidPrice = bidPrice;
            _stockPercentageValue = stockPercentageValue;
            if (stockIncreaseOrDecrease != null)
            {
                _stockIncreaseOrDecrease = stockIncreaseOrDecrease;
            }
        }
    }
}

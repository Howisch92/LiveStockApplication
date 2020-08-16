using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveStockApplication.Models
{
    public class StockViewModel
    {
        public int id { get; set; }
        public string name { get; set; }

        public double bidPrice { get; set; }

        public double askPrice { get; set; }

        public StockViewModel(int id,string name, double bidPrice, double askPrice)
        {
            this.id = id;
            this.name = name;
            this.bidPrice = bidPrice;
            this.askPrice = askPrice;
        }
    }
}

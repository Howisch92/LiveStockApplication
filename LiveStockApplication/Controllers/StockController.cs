using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveStockApplication.Models;
using LiveStockApplication.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LiveStockApplication.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockService _stockServiceInstance;
        public StockController(IStockService _stockService)
        {
            _stockServiceInstance = _stockService;
        }


        [HttpGet]
        public IList<StockViewModel> OpenStockmarket()
        {
            _stockServiceInstance.OpenStockMarket();

            return _stockServiceInstance.GetListOfAllStocks();

        }
    }
}


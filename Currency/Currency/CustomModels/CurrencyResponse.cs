using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Currency.CustomModels
{
    public class CurrencyResponse
    {

        public string Buy { get; set; }
        public string Sell { get; set; }
        public string Date { get; set; }       

  

        public  CurrencyResponse(string Buy,string Sell, string Date)
        {
            this.Buy = Buy;
            this.Sell = Sell;
            this.Date = Date;

        }


    }
}

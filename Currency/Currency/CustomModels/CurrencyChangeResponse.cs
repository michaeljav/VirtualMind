using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Currency.Models
{
    public class CurrencyChangeResonse
    {
        public int UseId { get; set; }

        public string UseName { get; set; }

        public string CbuyCurrencyOrigenType { get; set; }
     
        public decimal CbuyCurrencyOrigenAmount { get; set; }
      
        public string CbuyCurrencyToBuyType { get; set; }       
        
        public decimal CbuyCurrencyToBuyRate { get; set; }
        
        public decimal CbuyCurrencyToBuyAmountCurrencyChanged { get; set; }
        
        public bool IslimitExceeded { get; set; }
        public decimal ValueLimit { get; set; }

        public decimal MaxValueToExchange { get; set; }

        public DateTime CbuyCreateDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Currency.CustomModels
{
    public class SP_GetTotalMonth
    {
        [Key]
        public int Use_Id { get; set; }
        public string CBuy_CurrencyToBuyType { get; set; }
        public Decimal TotalMonth { get; set; }
        public string  DateYearMonth { get; set; }

        
    }
}

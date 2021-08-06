using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Currency.Models
{
    public class CurrencyChangeRequest
    {
        [Required]
        public string CbuyCurrencyOrigenAmount { get; set; }

        [Required]
        public string CbuyCurrencyToBuyType { get; set; }
        [Required]
        public bool toSave { get; set; }
    }
}

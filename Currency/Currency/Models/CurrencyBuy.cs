using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Currency.Models
{
    [Table("CurrencyBuy")]
    public partial class CurrencyBuy
    {
        [Key]
        [Column("CBuy_Id")]
        public long CbuyId { get; set; }
        [Column("Use_Id")]
        public int UseId { get; set; }
        [Required]
        [Column("Use_Name")]
        [StringLength(100)]
        public string UseName { get; set; }
        [Required]
        [Column("CBuy_CurrencyOrigenType")]
        [StringLength(100)]
        public string CbuyCurrencyOrigenType { get; set; }
        [Column("CBuy_CurrencyOrigenAmount", TypeName = "decimal(10, 3)")]
        public decimal CbuyCurrencyOrigenAmount { get; set; }
        [Required]
        [Column("CBuy_CurrencyToBuyType")]
        [StringLength(100)]
        public string CbuyCurrencyToBuyType { get; set; }
        [Column("CBuy_CurrencyToBuyRate", TypeName = "decimal(10, 3)")]
        public decimal CbuyCurrencyToBuyRate { get; set; }
        [Column("CBuy_CurrencyToBuyAmountCurrencyChanged", TypeName = "decimal(10, 3)")]
        public decimal CbuyCurrencyToBuyAmountCurrencyChanged { get; set; }
        [Column("CBuy_CreateDate", TypeName = "datetime")]
        public DateTime CbuyCreateDate { get; set; }

        [ForeignKey(nameof(UseId))]
        [InverseProperty(nameof(User.CurrencyBuys))]
        public virtual User Use { get; set; }
    }
}

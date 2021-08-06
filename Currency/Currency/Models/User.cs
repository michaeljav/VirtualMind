using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Currency.Models
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
            CurrencyBuys = new HashSet<CurrencyBuy>();
        }

        [Key]
        [Column("Use_Id")]
        public int UseId { get; set; }
        [Required]
        [Column("Use_UserName")]
        [StringLength(100)]
        public string UseUserName { get; set; }
        [Required]
        [Column("Use_Password")]
        [StringLength(100)]
        public string UsePassword { get; set; }
        [Required]
        [Column("Use_Name")]
        [StringLength(100)]
        public string UseName { get; set; }
        [Column("Use_CreateDate", TypeName = "datetime")]
        public DateTime UseCreateDate { get; set; }
        [Column("Use_VersionDate", TypeName = "datetime")]
        public DateTime? UseVersionDate { get; set; }

        [InverseProperty(nameof(CurrencyBuy.Use))]
        public virtual ICollection<CurrencyBuy> CurrencyBuys { get; set; }
    }
}

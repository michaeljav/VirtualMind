using System;
using Currency.CustomModels;
using Currency.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Currency.Data

{
    public partial class CurrencyContext : DbContext
    {
        public CurrencyContext()
        {
        }

        public CurrencyContext(DbContextOptions<CurrencyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CurrencyBuy> CurrencyBuys { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<SP_GetTotalMonth> sp_GetTotalMonth { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CurrencyBuy>(entity =>
            {
                entity.HasOne(d => d.Use)
                    .WithMany(p => p.CurrencyBuys)
                    .HasForeignKey(d => d.UseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CurrencyBuy_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

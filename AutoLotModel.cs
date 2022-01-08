using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AutoLotModel
{
    public partial class AutoLotEntitiesModel : DbContext
    {
        public AutoLotEntitiesModel()
            : base("name=AutoLotModel")
        {
        }

        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Cont> Conts { get; set; }
        public virtual DbSet<Tranzactii> Tranzactiis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cont>()
                .Property(e => e.sold)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Cont>()
                .HasMany(e => e.Cards)
                .WithOptional(e => e.Cont)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Cont>()
                .HasMany(e => e.Tranzactiis)
                .WithOptional(e => e.Cont)
                .WillCascadeOnDelete();
        }
    }
}

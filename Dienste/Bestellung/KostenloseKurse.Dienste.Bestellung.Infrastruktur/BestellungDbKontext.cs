using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Bestellung.Infrastruktur
{
    public class BestellungDbKontext:DbContext 
    {
        public const string DEFAULT_SCHEMA = "bestellung";

        public BestellungDbKontext(DbContextOptions<BestellungDbKontext> options) : base(options)
        {
        }

        public DbSet<Domain.BestellungsAggregate.Bestellung> Bestellungen { get; set; }
        public DbSet<Domain.BestellungsAggregate.BestellungsArtikel> BestellungsArtikel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.BestellungsAggregate.Bestellung>().ToTable("Bestellungen", DEFAULT_SCHEMA);
            modelBuilder.Entity<Domain.BestellungsAggregate.BestellungsArtikel>().ToTable("BestellungsArtikel", DEFAULT_SCHEMA);

            modelBuilder.Entity<Domain.BestellungsAggregate.BestellungsArtikel>().Property(x => x.Preis).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Domain.BestellungsAggregate.Bestellung>().OwnsOne(o => o.Adresse).WithOwner();

            base.OnModelCreating(modelBuilder);
        }
    }
}

﻿// <auto-generated />
using System;
using KostenloseKurse.Dienste.Bestellung.Infrastruktur;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KostenloseKurse.Dienste.Bestellung.Infrastruktur.Migrations
{
    [DbContext(typeof(BestellungDbKontext))]
    [Migration("20220709163500_Initiale")]
    partial class Initiale
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KostenloseKurse.Dienste.Bestellung.Domain.BestellungsAggregate.Bestellung", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AbnehmerID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ErstellungsDatum")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Bestellungen", "bestellung");
                });

            modelBuilder.Entity("KostenloseKurse.Dienste.Bestellung.Domain.BestellungsAggregate.BestellungsArtikel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BestellungId")
                        .HasColumnType("int");

                    b.Property<string>("BildUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Preis")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProduktID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProduktName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BestellungId");

                    b.ToTable("BestellungsArtikel", "bestellung");
                });

            modelBuilder.Entity("KostenloseKurse.Dienste.Bestellung.Domain.BestellungsAggregate.Bestellung", b =>
                {
                    b.OwnsOne("KostenloseKurse.Dienste.Bestellung.Domain.BestellungsAggregate.Adresse", "Adresse", b1 =>
                        {
                            b1.Property<int>("BestellungId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Gebiet")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Linie")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PostleitZahl")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Provinz")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Strasse")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("BestellungId");

                            b1.ToTable("Bestellungen");

                            b1.WithOwner()
                                .HasForeignKey("BestellungId");
                        });

                    b.Navigation("Adresse");
                });

            modelBuilder.Entity("KostenloseKurse.Dienste.Bestellung.Domain.BestellungsAggregate.BestellungsArtikel", b =>
                {
                    b.HasOne("KostenloseKurse.Dienste.Bestellung.Domain.BestellungsAggregate.Bestellung", null)
                        .WithMany("BestellungsArtikel")
                        .HasForeignKey("BestellungId");
                });

            modelBuilder.Entity("KostenloseKurse.Dienste.Bestellung.Domain.BestellungsAggregate.Bestellung", b =>
                {
                    b.Navigation("BestellungsArtikel");
                });
#pragma warning restore 612, 618
        }
    }
}

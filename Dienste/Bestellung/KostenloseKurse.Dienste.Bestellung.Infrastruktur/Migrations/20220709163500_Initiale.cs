using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KostenloseKurse.Dienste.Bestellung.Infrastruktur.Migrations
{
    public partial class Initiale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bestellung");

            migrationBuilder.CreateTable(
                name: "Bestellungen",
                schema: "bestellung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ErstellungsDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adresse_Provinz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresse_Gebiet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresse_Strasse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresse_PostleitZahl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresse_Linie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AbnehmerID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestellungen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BestellungsArtikel",
                schema: "bestellung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProduktID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProduktName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BildUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Preis = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BestellungId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestellungsArtikel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BestellungsArtikel_Bestellungen_BestellungId",
                        column: x => x.BestellungId,
                        principalSchema: "bestellung",
                        principalTable: "Bestellungen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BestellungsArtikel_BestellungId",
                schema: "bestellung",
                table: "BestellungsArtikel",
                column: "BestellungId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BestellungsArtikel",
                schema: "bestellung");

            migrationBuilder.DropTable(
                name: "Bestellungen",
                schema: "bestellung");
        }
    }
}

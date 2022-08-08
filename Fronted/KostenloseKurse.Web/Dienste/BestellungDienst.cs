using KostenloseKurse.Shared.Dienste;
using KostenloseKurse.Shared.Düo;
using KostenloseKurse.Web.Dienste.Interfaces;
using KostenloseKurse.Web.Models.Bestellungen;
using KostenloseKurse.Web.Models.FakeZahlungen;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Dienste
{
    public class BestellungDienst : IBestellungDienst
    {
        private readonly IZahlungDienst _zahlungDienst;
        private readonly HttpClient _httpClient;
        private readonly IKorbDienst _korbDienst;
        private readonly ISharedIdentityDienst _sharedIdentityDienst;

        public BestellungDienst(IZahlungDienst zahlungDienst, HttpClient httpClient, IKorbDienst korbDienst, ISharedIdentityDienst sharedIdentityDienst)
        {
            _zahlungDienst = zahlungDienst;
            _httpClient = httpClient;
            _korbDienst = korbDienst;
            _sharedIdentityDienst = sharedIdentityDienst;
        }

        public async Task<BestellungErstellungViewModell> BestellungErstellen(CheckoutInfoEingabe checkoutInfoEingabe)
        {
            var korb = await _korbDienst.RufKorb();

            var zahlungsInfoEingabe = new ZahlungsInformationenEingabe()
            {
                KartenName = checkoutInfoEingabe.KartenName,
                KartenNummer = checkoutInfoEingabe.KartenNummer,
                Ablauf = checkoutInfoEingabe.Ablauf,
                CVV = checkoutInfoEingabe.CVV,
                GesamtPreis = korb.Gesamtpreis
            };
            var zahlungAntwort = await _zahlungDienst.ZahlungErhalten(zahlungsInfoEingabe);

            if (!zahlungAntwort)
            {
                return new BestellungErstellungViewModell() { Fehler = "Zahlung nicht erhalten", IstErfolgreich = false };
            }

            var bestellungErstellungEingabe = new BestellungErstellungEingabe()
            {
                AbnehmerID = _sharedIdentityDienst.RufBenutzerID,
                Addresse = new AdresseErstellungEingabe { Provinz = checkoutInfoEingabe.Provinz, 
                    Gebiet = checkoutInfoEingabe.Gebiet, Strasse = checkoutInfoEingabe.Strasse,
                    Linie = checkoutInfoEingabe.Linie, PostleitZahl = checkoutInfoEingabe.Postleitzahl },
            };

            korb.KorbGegenstande.ForEach(x =>
            {
                var bestellungGegenstande = new BestellungGegenstandErstellungEingabe { ProduktID = x.KursID,
                    Preis = x.RufAktuellenPreisAuf, BildUrl = "", ProduktName = x.KursName };
                bestellungErstellungEingabe.BestellungsArtikel.Add(bestellungGegenstande);
            });

            var antwort = await _httpClient.PostAsJsonAsync<BestellungErstellungEingabe>("bestellung", bestellungErstellungEingabe);

            if (!antwort.IsSuccessStatusCode)
            {
                return new BestellungErstellungViewModell() { Fehler = "Bestellung konnte nicht erstellt werden", IstErfolgreich = false };
            }

            var bestellungErstellungViewModell = await antwort.Content.ReadFromJsonAsync<Antwort<BestellungErstellungViewModell>>();

            bestellungErstellungViewModell.Daten.IstErfolgreich = true;
            await _korbDienst.KorbLöschen();
            return bestellungErstellungViewModell.Daten;
        }

        //Für Asynchroner Kontakt

        public Task<BestellungSuspendierenViewModell> BestellungSuspendieren(CheckoutInfoEingabe checkoutInfoEingabe)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<BestellungViewModell>> RufBestellungAuf()
        {
            var antwort = await _httpClient.GetFromJsonAsync<Antwort<List<BestellungViewModell>>>("bestellung");

            return antwort.Daten;
        }
    }
}

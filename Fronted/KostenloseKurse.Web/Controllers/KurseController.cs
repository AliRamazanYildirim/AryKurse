using KostenloseKurse.Shared.Dienste;
using KostenloseKurse.Web.Dienste.Interfaces;
using KostenloseKurse.Web.Models.Kataloge;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Controllers
{
    [Authorize]
    public class KurseController : Controller
    {
        private readonly IKatalogDienst _katalogDienst;
        private readonly ISharedIdentityDienst _sharedIdentityDienst;

        public KurseController(IKatalogDienst katalogDienst, ISharedIdentityDienst sharedIdentityDienst)
        {
            _katalogDienst = katalogDienst;
            _sharedIdentityDienst = sharedIdentityDienst;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _katalogDienst.RufAlleKurseNachBenutzerIDAufAsync(_sharedIdentityDienst.RufBenutzerID));
        }
        public async Task<IActionResult> Erstellen()
        {
            var kategorien = await _katalogDienst.RufAlleKategorienAufAsync();

            ViewBag.kategorieListe = new SelectList(kategorien, "ID", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Erstellen(KursEingabeErstellen kursEingabeErstellen)
        {
            var kategorien = await _katalogDienst.RufAlleKategorienAufAsync();
            ViewBag.kategorieListe = new SelectList(kategorien, "ID", "Name");
            if (!ModelState.IsValid)
            {
                return View();
            }
            kursEingabeErstellen.BenutzerID = _sharedIdentityDienst.RufBenutzerID;

            await _katalogDienst.KursErstellenAsync(kursEingabeErstellen);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Aktualisieren(string ID)
        {
            var kurs = await _katalogDienst.RufNachKursIDAuf(ID);
            var kategorien = await _katalogDienst.RufAlleKategorienAufAsync();

            if (kurs == null)
            {
                //Nachricht anzeigen
                RedirectToAction(nameof(Index));
            }
            ViewBag.kategorieListe = new SelectList(kategorien, "ID", "Name", kurs.ID);
            KursEingabeAktualisieren kursEingabeAktualisieren = new()
            {
                ID = kurs.ID,
                Name = kurs.Name,
                Bezeichnung = kurs.Bezeichnung,
                Preis = kurs.Preis,
                Eigenschaft = kurs.Eigenschaft,
                KategorieID = kurs.KategorieID,
                BenutzerID = kurs.BenutzerID,
                Bild = kurs.Bild
            };

            return View(kursEingabeAktualisieren);
        }

        [HttpPost]
        public async Task<IActionResult> Aktualisieren(KursEingabeAktualisieren kursEingabeAktualisieren)
        {
            var kategorien = await _katalogDienst.RufAlleKategorienAufAsync();
            ViewBag.kategorieListe = new SelectList(kategorien, "ID", "Name", kursEingabeAktualisieren.ID);
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _katalogDienst.KursAktualisierenAsync(kursEingabeAktualisieren);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Löschen(string ID)
        {
            await _katalogDienst.KursLöschenAsync(ID);

            return RedirectToAction(nameof(Index));
        }
    }
}

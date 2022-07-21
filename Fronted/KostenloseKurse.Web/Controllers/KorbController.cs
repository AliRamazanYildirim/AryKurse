using KostenloseKurse.Web.Dienste.Interfaces;
using KostenloseKurse.Web.Models.Korb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Controllers
{
    [Authorize]
    public class KorbController : Controller
    {
        private readonly IKatalogDienst _katalogDienst;
        private readonly IKorbDienst _korbDienst;

        public KorbController(IKatalogDienst katalogDienst, IKorbDienst korbDienst)
        {
            _katalogDienst = katalogDienst;
            _korbDienst = korbDienst;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _korbDienst.RufKorb());
        }
        public async Task<IActionResult> KorbGegenstandErstellen(string kursID)
        {
            var kurs = await _katalogDienst.RufNachKursIDAuf(kursID);

            var korbGegenstand = new KorbGegenstandViewModell { KursID = kurs.ID, KursName = kurs.Name, Preis = kurs.Preis };

            await _korbDienst.KorbGegenstandErstellen(korbGegenstand);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> KorbGegenstandEntfernen(string kursID)
        {
            var resultat = await _korbDienst.KorbGegenstandEntfernen(kursID);

            return RedirectToAction(nameof(Index));
        }


    }
}

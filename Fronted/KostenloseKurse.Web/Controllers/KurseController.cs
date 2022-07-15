using KostenloseKurse.Shared.Dienste;
using KostenloseKurse.Web.Dienste.Interfaces;
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
    }
}

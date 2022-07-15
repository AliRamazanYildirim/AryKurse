using KostenloseKurse.Web.Dienste.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Controllers
{
    [Authorize]
    public class BenutzerController : Controller
    {
        private readonly IBenutzerDienst _benutzerDienst;

        public BenutzerController(IBenutzerDienst benutzerDienst)
        {
            _benutzerDienst = benutzerDienst;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _benutzerDienst.RufNachBenutzerAuf());
        }
    }
}

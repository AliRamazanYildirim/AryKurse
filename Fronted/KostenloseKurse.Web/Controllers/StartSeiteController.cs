using KostenloseKurse.Web.Ausnahmen;
using KostenloseKurse.Web.Dienste.Interfaces;
using KostenloseKurse.Web.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Controllers
{
    public class StartSeiteController : Controller
    {
        private readonly ILogger<StartSeiteController> _logger;
        private readonly IKatalogDienst _katalogDienst;

        public StartSeiteController(ILogger<StartSeiteController> logger, IKatalogDienst katalogDienst)
        {
            _logger = logger;
            _katalogDienst = katalogDienst;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _katalogDienst.RufAlleKurseAufAsync());
        }

        public async Task<IActionResult> Einzelheit(string id)
        {
            return View(await _katalogDienst.RufNachKursIDAuf(id));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var fehlerEigenschaften = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (fehlerEigenschaften != null && fehlerEigenschaften.Error is NichtAutorisierteAusnahme)
            {
                return RedirectToAction(nameof(AuthController.Ausloggen), "Auth");
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

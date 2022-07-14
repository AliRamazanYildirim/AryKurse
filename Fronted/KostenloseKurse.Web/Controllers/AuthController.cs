using KostenloseKurse.Web.Dienste.Interfaces;
using KostenloseKurse.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityDienst _identityDienst;

        public AuthController(IIdentityDienst identityDienst)
        {
            _identityDienst = identityDienst;
        }

        public IActionResult Einloggen()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Einloggen(EingabeAnmelden eingabeAnmelden)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var antwort = await _identityDienst.Einloggen(eingabeAnmelden);

            if (!antwort.IstErfolgreich)
            {
                antwort.Fehler.ForEach(x =>
                {
                    ModelState.AddModelError(String.Empty, x);
                });

                return View();
            }

            return RedirectToAction(nameof(Index), "Home");
        }

        public async Task<IActionResult> Ausloggen()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _identityDienst.WiderrufenAktualisierungsToken();
            return RedirectToAction(nameof(StartSeiteController.Index), "Home");
        }

    }
}

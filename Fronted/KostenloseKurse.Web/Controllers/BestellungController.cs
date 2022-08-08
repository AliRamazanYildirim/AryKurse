using KostenloseKurse.Web.Dienste.Interfaces;
using KostenloseKurse.Web.Models.Bestellungen;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Controllers
{
    public class BestellungController : Controller
    {
        private readonly IKorbDienst _korbDienst;
        private readonly IBestellungDienst _bestellungDienst;

        public BestellungController(IKorbDienst korbDienst, IBestellungDienst bestellungDienst)
        {
            _korbDienst = korbDienst;
            _bestellungDienst = bestellungDienst;
        }

        public async Task<IActionResult> Checkout()
        {
            var korb = await _korbDienst.RufKorb();

            ViewBag.korb = korb;
            return View(new CheckoutInfoEingabe());
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoEingabe checkoutInfoEingabe)
        {
            //Erster Weg synchrone Kommunikation
            // var orderStatus = await _orderService.CreateOrder(checkoutInfoInput);
            // Zweiter Weg asynchrone Kommunikation

            var bestellungSuspendieren = await _bestellungDienst.BestellungSuspendieren(checkoutInfoEingabe);
            if (!bestellungSuspendieren.IstErfolgreich)
            {
                var korb = await _korbDienst.RufKorb();

                ViewBag.korb = korb;

                ViewBag.error = bestellungSuspendieren.Fehler;

                return View();
            }
            //Erster Weg synchrone Kommunikation
            //  return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = orderStatus.OrderId });

            //Zweiter Weg asynchrone Kommunikation
            return RedirectToAction(nameof(ErfolgreicheCheckout), new { bestellungID = new Random().Next(1, 1000) });
        }

        public IActionResult ErfolgreicheCheckout(int bestellungID)
        {
            ViewBag.bestellungID = bestellungID;
            return View();
        }

        public async Task<IActionResult> CheckoutHistory()
        {
            return View(await _bestellungDienst.RufBestellungAuf());
        }
    }
}

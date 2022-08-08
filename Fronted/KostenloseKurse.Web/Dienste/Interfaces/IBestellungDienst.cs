using KostenloseKurse.Web.Models.Bestellungen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Dienste.Interfaces
{
    public interface IBestellungDienst
    {
        /// <summary>
        /// Synchrone Kommunikation - Anfrage wird direkt an den Bestell-Microservice gestellt
        /// </summary>
        /// <param name="checkoutInfoEingabe"></param>
        /// <returns></returns>
        Task<BestellungErstellungViewModell> BestellungErstellen(CheckoutInfoEingabe checkoutInfoEingabe);

        /// <summary>
        /// Asynchroner Kontakt - Bestellinformationen werden an rabbitMQ gesendet
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        Task<BestellungSuspendierenViewModell> BestellungSuspendieren(CheckoutInfoEingabe checkoutInfoEingabe);

        Task<List<BestellungViewModell>> RufBestellungAuf();
    }
}

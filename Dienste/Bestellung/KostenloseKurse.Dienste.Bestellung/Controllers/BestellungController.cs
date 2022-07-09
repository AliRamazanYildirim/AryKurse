using KostenloseKurse.Dienste.Bestellung.Anwendung.Anfragen;
using KostenloseKurse.Dienste.Bestellung.Anwendung.Befehle;
using KostenloseKurse.Shared.ControllerBasis;
using KostenloseKurse.Shared.Dienste;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Bestellung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BestellungController : BrauchBasisController
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityDienst _sharedIdentityDienst;

        public BestellungController(IMediator mediator, ISharedIdentityDienst sharedIdentityDienst)
        {
            _mediator = mediator;
            _sharedIdentityDienst = sharedIdentityDienst;
        }

        [HttpGet]
        public async Task<IActionResult> BestellungenAufrufen()
        {
            var antwort = await _mediator.Send(new AbrufenVonBestellungenNachBenutzerIDAbfrage { BenutzerID = _sharedIdentityDienst.RufBenutzerID });

            return ErstellenAktionResultatBeispiel(antwort);
        }

        [HttpPost]
        public async Task<IActionResult> BestellungSpeichern(BestellungsBefehlErstellen bestellungsBefehlErstellen)
        {
            var antwort = await _mediator.Send(bestellungsBefehlErstellen);

            return ErstellenAktionResultatBeispiel(antwort);
        }
    }
}

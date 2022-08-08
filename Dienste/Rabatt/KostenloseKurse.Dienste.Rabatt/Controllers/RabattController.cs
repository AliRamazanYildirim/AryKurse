using KostenloseKurse.Dienste.Rabatt.Dienste;
using KostenloseKurse.Shared.ControllerBasis;
using KostenloseKurse.Shared.Dienste;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Rabatt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabattController : BrauchBasisController
    {
        private readonly IRabattDienst _rabattDienst;

        private readonly ISharedIdentityDienst _sharedIdentityDienst;

        public RabattController(IRabattDienst rabattDienst, ISharedIdentityDienst sharedIdentityDienst)
        {
            _rabattDienst = rabattDienst;
            _sharedIdentityDienst = sharedIdentityDienst;
        }

        [HttpGet]
        public async Task<IActionResult> RufenAlleDatenAuf()
        {
            return ErstellenAktionResultatBeispiel(await _rabattDienst.RufenAlleDatenAuf());
        }

        //api/rabatt/7
        [HttpGet("{ID}")]
        public async Task<IActionResult> RufenNachIDAuf(int ID)
        {
            var rabatt = await _rabattDienst.RufenNachIDAuf(ID);

            return ErstellenAktionResultatBeispiel(rabatt);
        }

        [HttpGet]
        [Route("/api/[controller]/[action]/{Code}")]
        public async Task<IActionResult> RufenNachCodeAuf(string Code)

        {
            var benutzerID = _sharedIdentityDienst.RufBenutzerID;

            var rabatt = await _rabattDienst.RufenNachCodeUndBenutzerIDAuf(Code, benutzerID);

            return ErstellenAktionResultatBeispiel(rabatt);
        }

        [HttpPost]
        public async Task<IActionResult> Speichern(Modell.Rabatt rabatt)
        {
            return ErstellenAktionResultatBeispiel(await _rabattDienst.Speichern(rabatt));
        }

        [HttpPut]
        public async Task<IActionResult> Aktualisieren(Modell.Rabatt rabatt)
        {
            return ErstellenAktionResultatBeispiel(await _rabattDienst.Aktualisieren(rabatt));
        }

        [HttpDelete("{ID}")]
        public async Task<IActionResult> Löschen(int ID)
        {
            return ErstellenAktionResultatBeispiel(await _rabattDienst.Löschen(ID));
        }
    }
}

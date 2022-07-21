using KostenloseKurse.Dienste.Katalog.Dienste;
using KostenloseKurse.Dienste.Katalog.Düo;
using KostenloseKurse.Shared.ControllerBasis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Katalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KurseController : BrauchBasisController
    {
        private readonly IKursDienst _kursDienst;

        public KurseController(IKursDienst kursDienst)
        {
            _kursDienst = kursDienst;
        }
        [HttpGet]
        public async Task<IActionResult> RufAlleDatenAuf()
        {
            var antwort = await _kursDienst.RufAlleDatenAufAsync();
            return ErstellenAktionResultatBeispiel(antwort);
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> RufNachIDAuf(string ID)
        {
            var antwort=await _kursDienst.RufNachIDAufAsync(ID);
            return ErstellenAktionResultatBeispiel(antwort);
        }
        [HttpGet]
        [Route("/api/[controller]/RufNachBenutzerIDAuf/{benutzerID}")]
        public async Task<IActionResult> RufNachBenutzerIDAuf(string benutzerID)
        {
            var antwort = await _kursDienst.RufNachBenutzerIDAufAsync(benutzerID);
            return ErstellenAktionResultatBeispiel(antwort);
        }

        [HttpPost]
        public async Task<IActionResult> KursErstellen(KursErstellenDüo kursErstellenDüo)
        {
            var antwort = await _kursDienst.KursErstellenAsync(kursErstellenDüo);
            return ErstellenAktionResultatBeispiel(antwort);
        }

        [HttpPut]
        public async Task<IActionResult> KursAktualisieren(KursAktualisierenDüo kursAktualisierenDüo)
        {
            var antwort = await _kursDienst.KursAktualisierenAsync(kursAktualisierenDüo);
            return ErstellenAktionResultatBeispiel(antwort);
        }

        [HttpDelete("{ID}")]
        public async Task<IActionResult> KursLöschen(string ID)
        {
            var antwort = await _kursDienst.KursLöschenAsync(ID);
            return ErstellenAktionResultatBeispiel(antwort);
        }
    }
}

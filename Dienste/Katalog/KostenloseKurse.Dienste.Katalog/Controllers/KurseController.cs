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
        public async Task<IActionResult> RufAlleDaten()
        {
            var antwort = await _kursDienst.RufAlleDatenAsync();
            return ErstellenAktionResultatBeispiel(antwort);
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult>RufZurID(string ID)
        {
            var antwort=await _kursDienst.RufZurIDAsync(ID);
            return ErstellenAktionResultatBeispiel(antwort);
        }
        [HttpGet]
        [Route("/api/[controller]/RufAlleZurBenutzerID/{benutzerID}")]
        public async Task<IActionResult> RufZurBenutzerID(string benutzerID)
        {
            var antwort = await _kursDienst.RufZurBenutzerIDAsync(benutzerID);
            return ErstellenAktionResultatBeispiel(antwort);
        }

        [HttpPost]
        public async Task<IActionResult> Erstellen(KursErstellenDüo kursErstellenDüo)
        {
            var antwort = await _kursDienst.ErstellenAsync(kursErstellenDüo);
            return ErstellenAktionResultatBeispiel(antwort);
        }

        [HttpPut]
        public async Task<IActionResult> Aktualisieren(KursAktualisierenDüo kursAktualisierenDüo)
        {
            var antwort = await _kursDienst.AktualisierenAsync(kursAktualisierenDüo);
            return ErstellenAktionResultatBeispiel(antwort);
        }

        [HttpDelete("{ID}")]
        public async Task<IActionResult> Löschen(string ID)
        {
            var antwort = await _kursDienst.LöschenAsync(ID);
            return ErstellenAktionResultatBeispiel(antwort);
        }
    }
}

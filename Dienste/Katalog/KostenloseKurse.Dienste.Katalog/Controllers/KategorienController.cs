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
    public class KategorienController : BrauchBasisController
    {
        private readonly IKategorieDienst _kategorieDienst;

        public KategorienController(IKategorieDienst kategorieDienst)
        {
            _kategorieDienst = kategorieDienst;
        }
        public async Task<IActionResult>RufAlleDaten()
        {
            var kategorien = await _kategorieDienst.RufAlleDatenAsync();
            return ErstellenAktionResultatBeispiel(kategorien);
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> Erstellen(KategorieDüo kategorieDüo)
        {
            var antwort = await _kategorieDienst.ErstellenAsync(kategorieDüo);
            return ErstellenAktionResultatBeispiel(antwort);
        }

    }
}

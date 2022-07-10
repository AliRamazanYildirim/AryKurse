using KostenloseKurse.Shared.ControllerBasis;
using KostenloseKurse.Shared.Düo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KostenloseKurse.Dienste.FakeZahlung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakeZahlungController : BrauchBasisController
    {
        [HttpPost]
        public IActionResult ZahlungErhalten()
        {
            return ErstellenAktionResultatBeispiel(Antwort<KeinInhaltDüo>.Erfolg(200));

        }
    }
}

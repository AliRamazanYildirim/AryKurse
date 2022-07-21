using KostenloseKurse.Dienste.Korb.Dienste;
using KostenloseKurse.Dienste.Korb.Düo;
using KostenloseKurse.Shared.ControllerBasis;
using KostenloseKurse.Shared.Dienste;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Korb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorbController : BrauchBasisController
    {
        private readonly IKorbDienst _korbDienst;
        private readonly ISharedIdentityDienst _sharedIdentityDienst;

        public KorbController(IKorbDienst korbDienst, ISharedIdentityDienst sharedIdentityDienst)
        {
            _korbDienst = korbDienst;
            _sharedIdentityDienst = sharedIdentityDienst;
        }
        [HttpGet]
        public async Task<IActionResult>RufKorb()
        {
            //var ansprüche = HttpContext.User.Claims;
            return ErstellenAktionResultatBeispiel(await _korbDienst.RufKorb(_sharedIdentityDienst.RufBenutzerID));
        }
        [HttpPost]
        public async Task<IActionResult>SpeichernOderAktualisieren(KorbDüo korbDüo)
        {
            korbDüo.BenuzterID = _sharedIdentityDienst.RufBenutzerID;
            var antwort = await _korbDienst.SpeichernOderAktualisieren(korbDüo);
            return ErstellenAktionResultatBeispiel(antwort);
        }
        [HttpDelete]
        public async Task<IActionResult>KorbLöschen()
        {
            return ErstellenAktionResultatBeispiel(await _korbDienst.KorbLöschen(_sharedIdentityDienst.RufBenutzerID));
        }

    }
}

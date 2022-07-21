using KostenloseKurse.Dienste.FotoBestand.Düo;
using KostenloseKurse.Shared.ControllerBasis;
using KostenloseKurse.Shared.Düo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.FotoBestand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotoBestandController : BrauchBasisController
    {
        [HttpPost]
        public async Task<IActionResult> FotoSpeichern(IFormFile bild, CancellationToken cancellationToken)
        {
            if (bild != null && bild.Length > 0)
            {
                var weg = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/bilder", bild.FileName);
                using var strom = new FileStream(weg, FileMode.Create);
                await bild.CopyToAsync(strom, cancellationToken);

                var rückWeg = bild.FileName;

                FotoDüo fotoDüo = new() { Url = rückWeg };
                return ErstellenAktionResultatBeispiel(Antwort<FotoDüo>.Erfolg(fotoDüo, 200));
            }
            return ErstellenAktionResultatBeispiel(Antwort<FotoDüo>.Fehlschlag("Foto ist leer", 400));
        }
        public IActionResult FotoLöschen(string bildUrl)
        {
            var weg = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/bilder", bildUrl);
            if (!System.IO.File.Exists(weg))
            {
                return ErstellenAktionResultatBeispiel(Antwort<KeinInhaltDüo>.Fehlschlag("Foto wurde nicht gefunden", 404));
            }

            System.IO.File.Delete(weg);

            return ErstellenAktionResultatBeispiel(Antwort<KeinInhaltDüo>.Erfolg(204));
        }
    }
}

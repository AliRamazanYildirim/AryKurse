using KostenloseKurse.IdentityServer.Düo;
using KostenloseKurse.IdentityServer.Models;
using KostenloseKurse.Shared.Düo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace KostenloseKurse.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BenutzerController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public BenutzerController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult>Anmelden(AnmeldenDüo anmeldenDüo)
        {
            var benutzer = new ApplicationUser
            {
                UserName = anmeldenDüo.BenutzerName,
                Email = anmeldenDüo.EMail,
                City = anmeldenDüo.City

            };
            var resultat = await _userManager.CreateAsync(benutzer, anmeldenDüo.Passwort);
            if(!resultat.Succeeded)
            {
                return BadRequest(Antwort<KeinInhaltDüo>.Fehlschlagen(resultat.Errors.Select(x => x.Description).ToList(), 400));
            }
            return NoContent();
        }
    }
}

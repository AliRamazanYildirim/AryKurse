using IdentityModel;
using IdentityServer4.Validation;
using KostenloseKurse.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KostenloseKurse.IdentityServer.Dienste
{
    public class IdentitaetsRessourcenbesitzerPasswortValidierer : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentitaetsRessourcenbesitzerPasswortValidierer(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existierterBenutzer = await _userManager.FindByEmailAsync(context.UserName);
            if(existierterBenutzer==null)
            {
                var fehler = new Dictionary<string, object>();
                fehler.Add("fehler",new List<string> { "Ihre Email oder Ihr Password ist falsch" });
                context.Result.CustomResponse = fehler;
                return;
            }
            var passwortÜberprüfung = await _userManager.CheckPasswordAsync(existierterBenutzer, context.Password);
            if (passwortÜberprüfung == false)
            {
                var fehler = new Dictionary<string, object>();
                fehler.Add("fehler", new List<string> { "Ihre Email oder Ihr Password ist falsch" });
                context.Result.CustomResponse = fehler;
                return;
            }
            context.Result = new GrantValidationResult(existierterBenutzer.Id.ToString(),
                OidcConstants.AuthenticationMethods.Password);
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KostenloseKurse.Shared.Dienste
{
    public class SharedIdentityDienst : ISharedIdentityDienst
    {
        private IHttpContextAccessor _httpContextAccessor;

        public SharedIdentityDienst(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string RufBenutzerID => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value;
            //Oder so kann man schreiben User.Claims.Where(x=>x.Type=="sub").FirstOrDefault().Value;
        
    }
}

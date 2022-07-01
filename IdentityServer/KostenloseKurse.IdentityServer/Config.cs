// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace KostenloseKurse.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("ressource_katalog"){Scopes={"katalog_volleerlaubnis"} },
            new ApiResource("fotobestand_katalog"){Scopes={ "fotobestand_volleerlaubnis" } },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                    
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("katalog_volleerlaubnis","Vollzugriff auf die Katalog-API"),
                new ApiScope("fotobestand_volleerlaubnis","Vollzugriff auf die Fotobestand-API"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
               new Client
               {
                   ClientName="Asp.Net Core MVC",
                   ClientId="WebMvcClient",
                   ClientSecrets={new Secret("geheimnis".Sha256()) },
                   AllowedGrantTypes=GrantTypes.ClientCredentials,
                   AllowedScopes={ "katalog_volleerlaubnis", "fotobestand_volleerlaubnis",IdentityServerConstants.LocalApi.ScopeName }
               }
            };
    }
}
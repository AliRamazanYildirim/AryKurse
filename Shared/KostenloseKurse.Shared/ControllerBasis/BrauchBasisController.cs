using KostenloseKurse.Shared.Düo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace KostenloseKurse.Shared.ControllerBasis
{
    public class BrauchBasisController:ControllerBase
    {
        public IActionResult ErstellenAktionResultatBeispiel<T>(Antwort<T> antwort)
        {
            return new ObjectResult(antwort)
            {
                StatusCode = antwort.StatusCode 
            };
        }
    }
}

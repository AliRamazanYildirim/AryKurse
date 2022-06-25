using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace KostenloseKurse.Shared.Düo
{
    public class AntwortDüo<T>
    {
        public T Daten { get; set; }
        [JsonIgnore]//Ich habe JsonIgnore verwendet, da ich die Antwort auf die Liste nicht auch als Array serialisieren möchte.
        public int StatusCode { get; set; }
        [JsonIgnore]
        public bool IstErfolgreich { get; set; }
        public List<string> Fehler { get; set; }
        //Static Factory Method
        public static AntwortDüo<T> Erfolg(T daten,int statusCode)
        {
            return new AntwortDüo<T> { Daten = daten, StatusCode = statusCode, IstErfolgreich=true };
        }
        public static AntwortDüo<T> Erfolg(int statusCode)
        {
            return new AntwortDüo<T>{ Daten=default(T),StatusCode=statusCode,IstErfolgreich=true};
        }
        public static AntwortDüo<T> Fehlschlagen(List<string> fehler,int statusCode )
        {
            return new AntwortDüo<T>
            {
                Fehler = fehler,
                StatusCode = statusCode,
                IstErfolgreich = true
            };
        }
        public static AntwortDüo<T>Fehlschlagen(string fehler, int statusCode)
        {
            return new AntwortDüo<T> { Fehler = new List<string>() { fehler}, StatusCode = statusCode, IstErfolgreich = false };
        }

    }
}

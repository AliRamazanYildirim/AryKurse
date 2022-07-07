using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace KostenloseKurse.Shared.Düo
{
    public class Antwort<T>
    {
        public T Daten { get; set; }
        [JsonIgnore]//Ich habe JsonIgnore verwendet, da ich die Antwort auf die Liste nicht auch als Array serialisieren möchte.
        public int StatusCode { get; set; }
        [JsonIgnore]
        public bool IstErfolgreich { get; set; }
        public List<string> Fehler { get; set; }
        //Static Factory Method
        public static Antwort<T> Erfolg(T daten,int statusCode)
        {
            return new Antwort<T> { Daten = daten, StatusCode = statusCode, IstErfolgreich=true };
        }
        public static Antwort<T> Erfolg(int statusCode)
        {
            return new Antwort<T>{ Daten=default(T),StatusCode=statusCode,IstErfolgreich=true};
        }
        public static Antwort<T> Fehlschlag(List<string> fehler,int statusCode )
        {
            return new Antwort<T>
            {
                Fehler = fehler,
                StatusCode = statusCode,
                IstErfolgreich = true
            };
        }
        public static Antwort<T>Fehlschlag(string fehler, int statusCode)
        {
            return new Antwort<T> { Fehler = new List<string>() { fehler}, StatusCode = statusCode, IstErfolgreich = false };
        }

    }
}

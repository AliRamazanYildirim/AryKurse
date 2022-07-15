using System;
using System.Runtime.Serialization;

namespace KostenloseKurse.Web.Ausnahmen
{
    public class NichtAutorisierteAusnahme : Exception
    {
        public NichtAutorisierteAusnahme()
        {
        }

        public NichtAutorisierteAusnahme(string message) : base(message)
        {
        }

        public NichtAutorisierteAusnahme(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}

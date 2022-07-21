using FluentValidation;
using KostenloseKurse.Web.Models.Kataloge;

namespace KostenloseKurse.Web.Validierer
{
    public class KursEingabeAktualisierenValidierer : AbstractValidator<KursEingabeAktualisieren>
    {
        public KursEingabeAktualisierenValidierer()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Namensraum darf nicht leer sein");
            RuleFor(x => x.Bezeichnung).NotEmpty().WithMessage("Beschreibungsfeld darf nicht leer sein");
            RuleFor(x => x.Eigenschaft.Dauer).InclusiveBetween(1, int.MaxValue).WithMessage("Dauersfeld darf nicht leer sein");

            RuleFor(x => x.Preis).NotEmpty().WithMessage("Preisfeld darf nicht leer sein").ScalePrecision(2, 6)
                .WithMessage("Sie haben das falsche Währungsformat eingegeben");
            
        }
    }
}

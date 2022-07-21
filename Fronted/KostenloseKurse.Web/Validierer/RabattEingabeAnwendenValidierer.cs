using FluentValidation;
using KostenloseKurse.Web.Models.Rabatt;

namespace KostenloseKurse.Web.Validierer
{
    public class RabattEingabeAnwendenValidierer : AbstractValidator<RabattEingabeAnwenden>
    {
        public RabattEingabeAnwendenValidierer()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Das Rabattgutscheinfeld darf nicht leer sein");
        }
    }
}

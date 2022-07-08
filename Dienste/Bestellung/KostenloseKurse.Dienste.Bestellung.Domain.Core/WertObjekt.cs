using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Bestellung.Domain.Core
{
    public abstract class WertObjekt
    {
        protected static bool EqualOperator(WertObjekt left, WertObjekt right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(WertObjekt left, WertObjekt right)
        {
            return !(EqualOperator(left, right));
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (WertObjekt)obj;

            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
             .Select(x => x != null ? x.GetHashCode() : 0)
             .Aggregate((x, y) => x ^ y);
        }

        public WertObjekt GetCopy()
        {
            return this.MemberwiseClone() as WertObjekt;
        }
    }
}

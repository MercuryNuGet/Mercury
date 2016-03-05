using System;

namespace Mercury
{
    [Obsolete("Please descend from MercurySuite now and override Specifications rather than Cases")]
    public abstract class SpecificationByMethod : MercurySuite
    {
        protected override void Specifications()
        {
            Cases();
        }

        protected abstract void Cases();
    }
}
using NUnit.Framework;
using System;

namespace Mercury
{
    public abstract class MercurySuite : m
    {
        private SpecSet _specs = new SpecSet();

        /// <summary>
        /// Add new spec
        /// </summary>
        /// <param name="specification"></param>
        protected void Spec(ISpecification specification)
        {
            _specs += specification;
        }

        /// <summary>
        /// Use += to add ISpecifications
        /// </summary>
        public SpecSet Specs
        {
            get { return _specs; }
            set { _specs = value; }
        }

        /// <summary>
        /// Call Spec for each new specification inside this method.
        /// </summary>
        protected abstract void Specifications();

        protected override ISpecification[] TestCases()
        {
            _specs = new SpecSet();
            try
            {
                Specifications();
            }
            catch (Exception ex)
            {
                Spec((GetType().Name + " failed to add tests")
                    .Assert(() => Assert.Fail(ex.ToString())));
            }
            return _specs.ToArray();
        }
    }
}
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Mercury
{
    public abstract class SpecificationByMethod : Specification
    {
        private readonly List<ISpecification> _specs = new List<ISpecification>();

        /// <summary>
        /// Add new spec
        /// </summary>
        /// <param name="specification"></param>
        protected void Spec(ISpecification specification)
        {
            _specs.Add(specification);
        }

        /// <summary>
        /// Call Spec for each new specification inside this method.
        /// </summary>
        protected abstract void Cases();

        protected override ISpecification[] TestCases()
        {
            _specs.Clear();
            try
            {
                Cases();
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
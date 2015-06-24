using Mercury;
using NUnit.Framework;

namespace MercuryExamples
{
    internal class MultiDataArguments : Specification
    {
        protected override ISpecification[] TestCases()
        {
            return new ISpecification[]
            {
                "1 items multi data with".Arrange(() => ">")
                    .With(1, 2, ">12")
                    .With(1, 23, ">123")
                    .Act((s, a, b, e) => s + a + b)
                    .Assert((combined, a, b, e) => Assert.AreEqual(e, combined)),
                "2 items multi data with".Arrange(() => ">")
                    .With(1, 2, 3, ">123")
                    .With(1, 2, 34, ">1234")
                    .Act((s, a, b, c, e) => s + a + b + c)
                    .Assert((combined, a, b, c, e) => Assert.AreEqual(e, combined)),
                "3 items multi data with".Arrange(() => ">")
                    .With(1, 2, 3, 4, ">1234")
                    .With(1, 2, 3, 45, ">12345")
                    .Act((s, a, b, c, d, e) => s + a + b + c + d)
                    .Assert((combined, a, b, c, d, e) => Assert.AreEqual(e, combined))
            };
        }
    }
}
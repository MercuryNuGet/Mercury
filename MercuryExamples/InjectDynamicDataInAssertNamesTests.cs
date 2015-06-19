using Mercury;
using NUnit.Framework;

namespace MercuryExamples
{
    internal class InjectDynamicDataInAssertNamesTests : Specification
    {
        protected override ISpecification[] TestCases()
        {
            return new ISpecification[]
            {
                "Can add #a to #b and get #c"
                    .Arrange<MathHelper>()
                    .With(new {a = 1, b = 2, c = 3})
                    .With(new {a = 5, b = 6, c = 11})
                    .Act((sut, d) => sut.Add(d.a, d.b))
                    .Assert((actual, d) => Assert.AreEqual(d.c, actual))
                    .Assert("and not -#c!", (actual, d) => Assert.AreNotEqual(-d.c, actual))
            };
        }
    }
}
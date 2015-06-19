using Mercury;
using NUnit.Framework;

namespace MercuryExamples
{
    internal class DynamicContextTests : Specification
    {
        protected override ISpecification[] TestCases()
        {
            return new ISpecification[]
            {
                "Can append #append and get #expect"
                    .Arrange<dynamic>(() => new {mock = "test"})
                    .With(new {append = 0, expect = "test0"})
                    .With(new {append = 1, expect = "test1"})
                    .Act((c, d) => c.mock + d.append)
                    .Assert((s, o) => Assert.AreEqual(o.expect, s)),
            };
        }
    }
}
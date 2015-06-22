using System.IO;
using Mercury;
using NUnit.Framework;

namespace MercuryTests.Extensions
{
    internal class ArrangeTests : Specification
    {
        protected override ISpecification[] TestCases()
        {
            return new ISpecification[]
            {
                "Blank arrange"
                    .Arrange()
                    .Assert("gives null test context", testContext => Assert.IsNull(testContext)),
                "No context needed because acting on static method"
                    .Arrange()
                    .Act(n => Path.Combine("a", "b"))
                    .Assert(path => Assert.AreEqual(@"a\b", path))
            };
        }
    }
}
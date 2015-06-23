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
                "Blank arrange no assert message"
                    .Arrange()
                    .Assert(Assert.IsNull),
                "Blank arrange"
                    .Arrange()
                    .Assert("gives null test context", Assert.IsNull),
                "No context needed because acting on static method"
                    .Arrange()
                    .Act(n => Path.Combine("a", "b"))
                    .Assert(path => Assert.AreEqual(@"a\b", path))
            };
        }
    }
}
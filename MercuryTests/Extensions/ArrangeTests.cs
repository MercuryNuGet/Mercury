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
                    .ArrangeNull()
                    .Assert(Assert.IsNull),
                "Blank arrange"
                    .ArrangeNull()
                    .Assert("gives null test context", Assert.IsNull),
                "No context needed because acting on static method"
                    .ArrangeNull()
                    .Act(n => Path.Combine("a", "b"))
                    .Assert(path => Assert.AreEqual(@"a\b", path))
            };
        }
    }
}
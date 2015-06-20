using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mercury;
using NUnit.Framework;

namespace MercuryTests.Extensions
{
    class ArrangeTests : Specification
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
                  .Act(n => System.IO.Path.Combine("a", "b"))
                  .Assert(path => Assert.AreEqual(@"a\b", path)),
            };
        }
    }
}

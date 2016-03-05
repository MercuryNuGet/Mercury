using System;
using System.IO;
using Mercury;
using NUnit.Framework;

namespace MercuryTests.Extensions
{
    internal class ArrangeTests : MercurySuite
    {
        protected override void Specifications()
        {
            Specs +=
                "Blank arrange no assert message"
                    .ArrangeNull()
                    .Assert(Assert.IsNull);

            Specs +=
                "Blank arrange"
                    .ArrangeNull()
                    .Assert("gives null test context", Assert.IsNull);

            Specs +=
                "No context needed because acting on static method"
                    .ArrangeNull()
                    .Act(n => string.Join(",", "a", "b"))
                    .Assert(path => Assert.AreEqual("a,b", path));
        }
    }
}
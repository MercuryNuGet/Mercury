using System;
using Mercury;
using NUnit.Framework;

namespace MercuryExamples
{
    public class ActTests : MercurySuite
    {
        protected override void Specifications()
        {
            Specs +=
                "Can act"
                    .Arrange(() => new MathHelper())
                    .Act(sut => sut.Add(5, 9))
                    .Assert(result => Assert.AreEqual(14, result));

            Specs +=
                "When I add the result is"
                    .Arrange(() => new MathHelper())
                    .Act(sut => sut.Add(5, 9))
                    .Assert("14", result => Assert.AreEqual(14, result))
                    .Assert("even", result => Assert.IsTrue(result % 2 == 0));

            Specs +=
                "When I add the result is"
                    .Arrange(() => new MathHelper())
                    .Act(sut => sut.Add(5, 9))
                    .AssertEquals(14);
        }
    }
}
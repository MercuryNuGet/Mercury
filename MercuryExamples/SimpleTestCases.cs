using System;
using Mercury;
using NUnit.Framework;

namespace MercuryExamples
{
    public class SimpleTestCases : MercurySuite
    {
        protected override void Specifications()
        {
            Specs +=
                "Can add two integers"
                    .Arrange(() => new MathHelper())
                    .Assert(sut => Assert.AreEqual(3, sut.Add(1, 2)));

            Specs +=
                "Can add two other integers"
                    .Arrange(() => new MathHelper())
                    .Assert(sut => Assert.AreEqual(14, sut.Add(5, 9)));

            Specs +=
                "I don't need to specify sut"
                    .Assert(() => Assert.AreEqual(3, 1 + 2));

            Specs +=
                "Can repeat do"
                    .Arrange(() => new MathHelper())
                    .Assert(sut => Assert.AreEqual(14, sut.Add(5, 9)))
                    .Assert(sut => Assert.AreEqual(10, sut.Add(5, 5)));

            Specs +=
                "Can repeat do with name"
                    .Arrange(() => new MathHelper())
                    .Assert("case 1", sut => Assert.AreEqual(14, sut.Add(5, 9)))
                    .Assert("case 2", sut => Assert.AreEqual(10, sut.Add(5, 5)));
        }
    }
}
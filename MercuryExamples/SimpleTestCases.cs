using Mercury;
using NUnit.Framework;

namespace MercuryExamples
{
    public class SimpleTestCases : Specification
    {
        protected override ISpecification[] TestCases()
        {
            return new ISpecification[]
            {
                "Can add two integers"
                    .Arrange(() => new MathHelper())
                    .Assert(sut => Assert.AreEqual(3, sut.Add(1, 2))),
                "Can add two other integers"
                    .Arrange(() => new MathHelper())
                    .Assert(sut => Assert.AreEqual(14, sut.Add(5, 9))),
                "I don't need to specify sut"
                    .Assert(() => Assert.AreEqual(3, 1 + 2)),
                "Can repeat do"
                    .Arrange(() => new MathHelper())
                    .Assert(sut => Assert.AreEqual(14, sut.Add(5, 9)))
                    .Assert(sut => Assert.AreEqual(10, sut.Add(5, 5))),
                "Can repeat do with name"
                    .Arrange(() => new MathHelper())
                    .Assert("case 1", sut => Assert.AreEqual(14, sut.Add(5, 9)))
                    .Assert("case 2", sut => Assert.AreEqual(10, sut.Add(5, 5)))
            };
        }
    }
}
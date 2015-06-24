using Mercury;
using NUnit.Framework;

namespace MercuryExamples
{
    internal class BranchExample : Specification
    {
        protected override ISpecification[] TestCases()
        {
            return new[]
            {
                "A:".Arrange<Sut>()
                    .ActOn(s => s.Value++)
                    .Assert(s => Assert.AreEqual(1, s.Value))
                    .Branch("B:", a =>
                        new[]
                        {
                            a.ActOn(s => s.Value += 3)
                                .Assert("1:", s => Assert.AreEqual(4, s.Value)),
                            a.ActOn(s => s.Value += 4)
                                .Assert("2:", s => Assert.AreEqual(5, s.Value))
                                .Branch("C:", b =>
                                    new ISpecification[]
                                    {
                                        b.ActOn(s => s.Value += 5)
                                            .Assert("1:", s => Assert.AreEqual(10, s.Value)),
                                        b.ActOn(s => s.Value += 3)
                                            .Assert("2:", s => Assert.AreEqual(8, s.Value))
                                    })
                        }
                    )
            };
        }
    }

    internal class Sut
    {
        public int Value { get; set; }
    }
}
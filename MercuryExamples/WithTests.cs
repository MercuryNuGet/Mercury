using Mercury;
using NUnit.Framework;

namespace MercuryExamples
{
    public class WithTests : MercurySuite
    {
        protected override void Specifications()
        {
            Specs +=
                "Can use data"
                    .Arrange<MathHelper>()
                    .With(new { x = 1, y = 2, r = 3 })
                    .Assert((sut, d) => Assert.AreEqual(d.r, sut.Add(d.x, d.y)));

            Specs +=
                "Can add #x and #y and get #r"
                    .Arrange<MathHelper>()
                    .With(new { x = 8, y = 2, r = 10 })
                    .With(new { x = 1, y = 2, r = 3 })
                    .With(new { x = 10, y = 20, r = 30 })
                    .Assert((sut, d) => Assert.AreEqual(d.r, sut.Add(d.x, d.y)));

            Specs +=
                "Can add #x and #y and get #r and multiassert"
                    .Arrange<MathHelper>()
                    .With(new { x = 8, y = 2, r = 10 })
                    .With(new { x = 1, y = 2, r = 3 })
                    .With(new { x = 10, y = 20, r = 30 })
                    .Assert((sut, d) => Assert.AreEqual(d.r, sut.Add(d.x, d.y)))
                    .Assert((sut, d) => Assert.AreEqual(d.r, sut.Add(d.x, d.y)));

            Specs +=
                "Can add #x and #y and get #r and multi named assert"
                    .Arrange<MathHelper>()
                    .With(new { x = 8, y = 2, r = 10 })
                    .With(new { x = 1, y = 2, r = 3 })
                    .With(new { x = 10, y = 20, r = 30 })
                    .Assert("Case A", (sut, d) => Assert.AreEqual(d.r, sut.Add(d.x, d.y)))
                    .Assert("Case B", (sut, d) => Assert.AreEqual(d.r, sut.Add(d.x, d.y)));

            Specs +=
                "Can add #x and #y and get #expected using act and single with"
                    .Arrange<MathHelper>()
                    .With(new { x = 8, y = 2, expected = 10 })
                    .Act((sut, d) => sut.Add(d.x, d.y))
                    .Assert((actual, d) => Assert.AreEqual(actual, d.expected));

            Specs +=
                "Can add #x and #y and get #expected using act"
                    .Arrange<MathHelper>()
                    .With(new { x = 8, y = 2, expected = 10 })
                    .With(new { x = 1, y = 2, expected = 3 })
                    .With(new { x = 10, y = 20, expected = 30 })
                    .Act((sut, d) => sut.Add(d.x, d.y))
                    .Assert((actual, d) => Assert.AreEqual(actual, d.expected));
        }
    }
}
using Mercury;
using NUnit.Framework;

namespace MercuryExamples
{
    internal class MultiDataArguments : MercurySuite
    {
        protected override void Specifications()
        {
            Specs +=
                   "3 items multi data with"
                    .Arrange(() => ">")
                    .With(1, 2, ">12")
                    .With(1, 23, ">123")
                    .Act((s, a, b, e) => s + a + b)
                    .Assert((combined, a, b, e) => Assert.AreEqual(e, combined));

            Specs +=
                "4 items multi data with"
                    .Arrange(() => ">")
                    .With(1, 2, 3, ">123")
                    .With(1, 2, 34, ">1234")
                    .Act((s, a, b, c, e) => s + a + b + c)
                    .Assert((combined, a, b, c, e) => Assert.AreEqual(e, combined));

            Specs +=
                "5 items multi data with"
                    .Arrange(() => ">")
                    .With(1, 2, 3, 4, ">1234")
                    .With(1, 2, 3, 45, ">12345")
                    .Act((s, a, b, c, d, e) => s + a + b + c + d)
                    .Assert((combined, a, b, c, d, e) => Assert.AreEqual(e, combined));

            Specs +=
                "3 items multi data with - single data equivalent"
                    .Arrange(() => ">")
                    .With(new { a = 1, b = 2, e = ">12" })
                    .With(new { a = 1, b = 23, e = ">123" })
                    .Act((s, data) => s + data.a + data.b)
                    .Assert((combined, data) => Assert.AreEqual(data.e, combined));

            Specs +=
                "4 items multi data with - single data equivalent"
                    .Arrange(() => ">")
                    .With(new { a = 1, b = 2, c = 3, e = ">123" })
                    .With(new { a = 1, b = 2, c = 34, e = ">1234" })
                    .Act((s, data) => s + data.a + data.b + data.c)
                    .Assert((combined, data) => Assert.AreEqual(data.e, combined));

            Specs +=
                "5 items multi data with - single data equivalent"
                    .Arrange(() => ">")
                    .With(new { a = 1, b = 2, c = 3, d = 4, e = ">1234" })
                    .With(new { a = 1, b = 2, c = 3, d = 45, e = ">12345" })
                    .Act((s, data) => s + data.a + data.b + data.c + data.d)
                    .Assert((combined, data) => Assert.AreEqual(data.e, combined));

            Specs +=
                "1 items multi data with - using extension assert"
                    .Arrange(() => "")
                    .With("1")
                    .With("1")
                    .Act((s, a) => s + a)
                    .Assert((combined, e) => Assert.AreEqual(e, combined));

            Specs +=
                "2 items multi data with - using extension assert"
                    .Arrange(() => ">")
                    .With(1, ">1")
                    .With(12, ">12")
                    .Act((s, a, e) => s + a)
                    .Assert((combined, e) => Assert.AreEqual(e, combined));

            Specs +=
                "3 items multi data with - using extension assert"
                    .Arrange(() => ">")
                    .With(1, 2, ">12")
                    .With(1, 23, ">123")
                    .Act((s, a, b, e) => s + a + b)
                    .Assert((combined, e) => Assert.AreEqual(e, combined));

            Specs +=
                "4 items multi data with - using extension assert"
                    .Arrange(() => ">")
                    .With(1, 2, 3, ">123")
                    .With(1, 2, 34, ">1234")
                    .Act((s, a, b, c, e) => s + a + b + c)
                    .Assert((combined, e) => Assert.AreEqual(e, combined));

            Specs +=
                "5 items multi data with - using extension assert"
                    .Arrange(() => ">")
                    .With(1, 2, 3, 4, ">1234")
                    .With(1, 2, 3, 45, ">12345")
                    .Act((s, a, b, c, d, e) => s + a + b + c + d)
                    .Assert((combined, e) => Assert.AreEqual(e, combined));

            Specs +=
                 "5 items multi data with - using extension assert and name"
                    .Arrange(() => ">")
                    .With(1, 2, 3, 4, ">1234")
                    .With(1, 2, 3, 45, ">12345")
                    .Act((s, a, b, c, d, e) => s + a + b + c + d)
                    .Assert("assert", (combined, e) => Assert.AreEqual(e, combined));

            Specs +=
                 "Concatenation of items"
                    .Arrange(() => ">")
                    .With(1, 2, 3, 4, ">1234")
                    .With(1, 2, 3, 45, ">12345")
                    .Act((s, a, b, c, d, e) => s + a + b + c + d)
                    .AssertEqualsExpected();
        }
    }
}
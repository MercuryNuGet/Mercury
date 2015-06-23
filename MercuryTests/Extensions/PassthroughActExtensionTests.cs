using System.Linq;
using Mercury;
using NUnit.Framework;

namespace MercuryTests.Extensions
{
    [TestFixture]
    public sealed class PassthroughActExtensionTests
    {
        [Test]
        public void Can_passthough()
        {
            TestUtil.RunAll("A".Arrange(() => 5)
                .Assert(sut => Assert.AreEqual(5, sut)));
        }

        [Test]
        public void Can_passthough_with_message()
        {
            TestUtil.RunAll("A".Arrange(() => 15)
                .Assert("message", sut => Assert.AreEqual(15, sut)));
        }

        [Test]
        public void Can_passthough_with_message_and_message_preserved()
        {
            Assert.AreEqual("A message",
                "A".Arrange(() => 15)
                    .Assert("message", sut => Assert.AreEqual(15, sut))
                    .EmitAllRunnableTests().Single().Name);
        }

        [Test]
        public void Can_passthough_with_data()
        {
            TestUtil.RunAll("A".Arrange(() => 6)
                .With(7)
                .Assert((sut, data) =>
                {
                    Assert.AreEqual(6, sut);
                    Assert.AreEqual(7, data);
                }));
        }

        [Test]
        public void Can_passthough_with_data_and_message()
        {
            TestUtil.RunAll("A".Arrange(() => 16)
                .With(17)
                .Assert("message", (sut, data) =>
                {
                    Assert.AreEqual(16, sut);
                    Assert.AreEqual(17, data);
                }));
        }

        [Test]
        public void Can_passthough_with_data_and_message_and_message_preserved()
        {
            Assert.AreEqual("A message",
                "A".Arrange(() => 15).With(8)
                    .Assert("message", (sut, value) => Assert.AreEqual(15, sut))
                    .EmitAllRunnableTests().Single().Name);
        }
    }
}
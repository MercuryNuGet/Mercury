using Mercury;
using NUnit.Framework;

namespace MercuryTests
{
    [TestFixture]
    public class DynamicNameInjectionTests
    {
        [Test]
        public void a()
        {
            Assert.AreEqual("", DynamicNameInjection.Inject("", new {}));
        }

        [Test]
        public void b()
        {
            Assert.AreEqual("some string", DynamicNameInjection.Inject("some string", new {}));
        }

        [Test]
        public void one_value()
        {
            Assert.AreEqual("2", DynamicNameInjection.Inject("#a", new {a = 2}));
        }

        [Test]
        public void two_values()
        {
            Assert.AreEqual("2 3", DynamicNameInjection.Inject("#a #b", new {a = 2, b = 3}));
        }

        [Test]
        public void clashing_values()
        {
            Assert.AreEqual("3 2 3", DynamicNameInjection.Inject("#aa #a #aa", new {a = 2, aa = 3}));
        }
    }
}
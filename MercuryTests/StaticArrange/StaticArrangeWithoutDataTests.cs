using System.IO;
using System.Linq;
using Mercury;
using NUnit.Framework;

namespace MercuryTests.StaticArrange
{
    [TestFixture]
    public sealed class StaticArrangeWithoutDataTests
    {
        private static void RunAll(ISpecification spec)
        {
            foreach (var test in spec.EmitAllRunnableTests())
                test.Run();
        }

        [Test]
        public void can_static_arrange_without_data()
        {
            ISpecification spec = "test"
                .StaticArrange()
                .Act(() => Path.Combine("a", "b"))
                .Assert(result => Assert.AreEqual("a\b", result));

            var tests = spec.EmitAllRunnableTests().ToArray();
            Assert.AreEqual(1, tests.Count());
            Assert.AreEqual("test", tests[0].Name);
        }

        [Test]
        public void can_static_arrange_without_data_double_asserts()
        {
            ISpecification spec = "test"
                .StaticArrange()
                .Act(() => Path.Combine("a", "b"))
                .Assert(result => Assert.AreEqual("a\b", result))
                .Assert(result => Assert.AreEqual("a\b", result));

            var tests = spec.EmitAllRunnableTests().ToArray();
            Assert.AreEqual(2, tests.Count());
            Assert.AreEqual("test", tests[0].Name);
            Assert.AreEqual("test", tests[1].Name);
        }

        [Test]
        public void can_static_arrange_without_data_double_asserts_with_names()
        {
            ISpecification spec = "test"
                .StaticArrange()
                .Act(() => Path.Combine("a", "b"))
                .Assert("first", result => Assert.AreEqual("a\b", result))
                .Assert("second", result => Assert.AreEqual("a\b", result));

            var tests = spec.EmitAllRunnableTests().ToArray();
            Assert.AreEqual(2, tests.Count());
            Assert.AreEqual("test first", tests[0].Name);
            Assert.AreEqual("test second", tests[1].Name);
        }

        [Test]
        public void calls_no_data_asserts()
        {
            var act = 0;
            var store = new int[4];
            ISpecification spec = "test"
                .StaticArrange()
                .Act(() => act++)
                .Assert(result => store[0]++)
                .Assert(result => store[1]++)
                .Assert("Named", result => store[2]++)
                .Assert("Named 2", result => store[3]++);

            RunAll(spec);
            Assert.AreEqual(4, store.Sum());
            Assert.IsTrue(store.All(s => s == 1));
            Assert.AreEqual(4, act);
        }
    }
}
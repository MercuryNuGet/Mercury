using System.IO;
using System.Linq;
using Mercury;
using NUnit.Framework;

namespace MercuryTests.StaticArrange
{
    [TestFixture]
    public sealed class StaticArrangeWithDataTests
    {
        private static void RunAll(ISpecification spec)
        {
            foreach (var test in spec.EmitAllRunnableTests())
                test.Run();
        }

        [Test]
        public void can_static_arrange_with_data()
        {
            ISpecification spec = "test"
                .StaticArrange()
                .With(new {a = "a", b = "b", expect = @"a\b"})
                .Act(data => Path.Combine(data.a, data.b))
                .Assert((result, data) => Assert.AreEqual(data.expect, result));

            var tests = spec.EmitAllRunnableTests().ToArray();
            Assert.AreEqual(1, tests.Count());
            Assert.AreEqual("test", tests[0].Name);
        }
        [Test]
        public void can_static_arrange_with_double_data()
        {
            ISpecification spec = "test"
                 .StaticArrange()
                 .With(new { a = "a", b = "b", expect = @"a\b" })
                 .With(new { a = "c", b = "d", expect = @"c\d" })
                 .Act(data => Path.Combine(data.a, data.b))
                 .Assert((result, data) => Assert.AreEqual(data.expect, result));

            var tests = spec.EmitAllRunnableTests().ToArray();
            Assert.AreEqual(2, tests.Count());
            Assert.AreEqual("test", tests[0].Name);
            Assert.AreEqual("test", tests[1].Name);
        }

        [Test]
        public void can_static_arrange_with_double_data_double_asserts()
        {
            ISpecification spec = "test"
                .StaticArrange()
                .With(new {a = "a", b = "b", expect = @"a\b"})
                .With(new {a = "c", b = "d", expect = @"c\d"})
                .Act(data => Path.Combine(data.a, data.b))
                .Assert((result, data) => Assert.AreEqual(data.expect, result))
                .Assert((result, data) => Assert.AreEqual(data.expect, result));

            var tests = spec.EmitAllRunnableTests().ToArray();
            Assert.AreEqual(4, tests.Count());
            Assert.AreEqual("test", tests[0].Name);
            Assert.AreEqual("test", tests[1].Name);
            Assert.AreEqual("test", tests[2].Name);
            Assert.AreEqual("test", tests[3].Name);
        }

        [Test]
        public void can_static_arrange_with_double_data_double_asserts_and_names()
        {
            ISpecification spec = "test"
                .StaticArrange()
                .With(new { a = "a", b = "b", expect = @"a\b" })
                .Act(data => Path.Combine(data.a, data.b))
                .Assert("first", (result, data) => Assert.AreEqual(data.expect, result))
                .Assert("second", (result, data) => Assert.AreEqual(data.expect, result));

            var tests = spec.EmitAllRunnableTests().ToArray();
            Assert.AreEqual(2, tests.Count());
            Assert.AreEqual("test first", tests[0].Name);
            Assert.AreEqual("test second", tests[1].Name);
        }

        [Test]
        public void with_one_data_and_one_assert_calls_each_once()
        {
            var actStore = new int[4];
            var store = 0;
            ISpecification spec = "test"
                .StaticArrange()
                .With(new {index = 1, value = 2})
                .Act(data => actStore[data.index] += data.value)
                .Assert((result, data) => store++);

            RunAll(spec);
            Assert.AreEqual(2, actStore[1]);
            Assert.AreEqual(2, actStore.Sum());
            Assert.AreEqual(1, store);
        }

        [Test]
        public void with_two_data_and_four_asserts_calls_each_correct_number_of_times()
        {
            var actStore = new int[4];
            var store = new int[4];
            ISpecification spec = "test"
                .StaticArrange()
                .With(new {index = 1, value = 2})
                .With(new {index = 2, value = 3})
                .Act(data => actStore[data.index] += data.value)
                .Assert((result, data) => store[0]++)
                .Assert((result, data) => store[1]++)
                .Assert("Named", (result, data) => store[2]++)
                .Assert("Named 2", (result, data) => store[3]++);

            RunAll(spec);

            const int expectedActInvokes = 4;
            const int expectedAssertInvokes = 2;
            Assert.AreEqual(2*expectedActInvokes, actStore[1]);
            Assert.AreEqual(3*expectedActInvokes, actStore[2]);
            Assert.AreEqual(5*expectedActInvokes, actStore.Sum());
            Assert.IsTrue(store.All(s => s == expectedAssertInvokes));
        }
    }
}
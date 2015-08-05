using System.IO;
using System.Linq;
using Mercury;
using NUnit.Framework;

namespace MercuryTests.Arrange
{
    [TestFixture]
    public sealed class ArrangeWithoutDataTests
    {
        public class Counter
        {
            public int Count { get; set; }
        }

        [Test]
        public void can_arrange_without_data()
        {
            ISpecification spec = "test"
                .Arrange<Counter>()
                .Act(counter => counter)
                .Assert(counter => { });

            var tests = spec.EmitAllRunnableTests().ToArray();
            Assert.AreEqual(1, tests.Count());
            Assert.AreEqual("test", tests[0].Name);
        }

        [Test]
        public void can_arrange_without_data_double_asserts()
        {
            ISpecification spec = "test"
                .Arrange<Counter>()
                .Act(counter => string.Join(",", "a", "b"))
                .Assert(result => Assert.AreEqual("a,b", result))
                .Assert(result => Assert.AreEqual("a,b", result));

            var tests = spec.EmitAllRunnableTests().ToArray();
            Assert.AreEqual(2, tests.Count());
            Assert.AreEqual("test", tests[0].Name);
            Assert.AreEqual("test", tests[1].Name);
        }

        [Test]
        public void can_arrange_without_data_double_asserts_with_names()
        {
            ISpecification spec = "test"
                .Arrange<Counter>()
                .Act(counter => string.Join(",", "a", "b"))
                .Assert("first", result => Assert.AreEqual("a,b", result))
                .Assert("second", result => Assert.AreEqual("a,b", result));

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
                .Arrange<Counter>()
                .Act(counter =>
                {
                    act++;
                    counter.Count++;
                    return counter;
                })
                .Assert(counter => store[0] += counter.Count)
                .Assert(counter => store[1] += counter.Count)
                .Assert("Named", result => store[2]++)
                .Assert("Named 2", result => store[3]++);

            TestUtil.RunAll(spec);
            Assert.AreEqual(4, store.Sum());
            Assert.IsTrue(store.All(s => s == 1));
            Assert.AreEqual(4, act);
        }

        [Test]
        public void reuse_of_post_act_should_give_two_separately_runnable_tests()
        {
            var actInvokes = 0;
            var store = new int[2];
            var builder = "test"
                .Arrange<Counter>()
                .Act(counter =>
                {
                    actInvokes++;
                    counter.Count++;
                    return counter;
                });

            ISpecification spec1 = builder
                .Assert(counter => store[0] += counter.Count);

            ISpecification spec2 = builder
                .Assert(counter => store[1] += counter.Count);

            TestUtil.RunAll(spec1);

            Assert.AreEqual(1, store[0]);
            Assert.AreEqual(0, store[1]);
            Assert.AreEqual(1, actInvokes);

            TestUtil.RunAll(spec2);

            Assert.AreEqual(1, store[0]);
            Assert.AreEqual(1, store[1]);
            Assert.AreEqual(2, actInvokes);
        }

        [Test]
        public void post_act_is_not_ISpecification_without_one_assert()
        {
            var builder = "test"
                .Arrange<Counter>()
                .Act(counter => 1);

            Assert.IsNotInstanceOf(typeof (ISpecification), builder);
        }
    }
}
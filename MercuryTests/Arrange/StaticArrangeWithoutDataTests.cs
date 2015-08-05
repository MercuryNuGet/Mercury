using System.IO;
using System.Linq;
using Mercury;
using NUnit.Framework;

namespace MercuryTests.Arrange
{
    [TestFixture]
    public sealed class StaticArrangeWithoutDataTests
    {
        [Test]
        public void can_static_arrange_without_data()
        {
            ISpecification spec = "test"
                .Arrange()
                .Act(() => string.Join(",", "a", "b"))
                .Assert(result => Assert.AreEqual("a,b", result));

            var tests = spec.EmitAllRunnableTests().ToArray();
            Assert.AreEqual(1, tests.Count());
            Assert.AreEqual("test", tests[0].Name);
        }

        [Test]
        public void can_static_arrange_without_data_double_asserts()
        {
            ISpecification spec = "test"
                .Arrange()
                .Act(() => string.Join(",", "a", "b"))
                .Assert(result => Assert.AreEqual("a,b", result))
                .Assert(result => Assert.AreEqual("a,b", result));

            var tests = spec.EmitAllRunnableTests().ToArray();
            Assert.AreEqual(2, tests.Count());
            Assert.AreEqual("test", tests[0].Name);
            Assert.AreEqual("test", tests[1].Name);
        }

        [Test]
        public void can_static_arrange_without_data_double_asserts_with_names()
        {
            ISpecification spec = "test"
                .Arrange()
                .Act(() => string.Join(",", "a", "b"))
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
                .Arrange()
                .Act(() => act++)
                .Assert(result => store[0]++)
                .Assert(result => store[1]++)
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
                .Arrange()
                .Act(() => actInvokes++);

            ISpecification spec1 = builder
                .Assert(result => store[0]++);

            ISpecification spec2 = builder
                .Assert(result => store[1]++);

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
                .Arrange()
                .Act(() => 1);

            Assert.IsNotInstanceOf(typeof (ISpecification), builder);
        }
    }
}
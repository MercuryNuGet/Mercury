﻿using System.IO;
using System.Linq;
using Mercury;
using NUnit.Framework;

namespace MercuryTests.Arrange
{
    [TestFixture]
    public sealed class StaticArrangeWithDataTests
    {
        [Test]
        public void can_static_arrange_with_data()
        {
            ISpecification spec = "test"
                .Arrange()
                .With(new {a = "a", b = "b", expect = "a,b"})
                .Act(data => string.Join(",", data.a, data.b))
                .Assert((result, data) => Assert.AreEqual(data.expect, result));

            var tests = spec.EmitAllRunnableTests().ToArray();
            Assert.AreEqual(1, tests.Count());
            Assert.AreEqual("test", tests[0].Name);
        }

        [Test]
        public void can_static_arrange_with_double_data()
        {
            ISpecification spec = "test"
                .Arrange()
                .With(new {a = "a", b = "b", expect = "a,b"})
                .With(new {a = "c", b = "d", expect = "c,d"})
                .Act(data => string.Join(",", data.a, data.b))
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
                .Arrange()
                .With(new {a = "a", b = "b", expect = "a,b"})
                .With(new {a = "c", b = "d", expect = "c,d"})
                .Act(data => string.Join(",", data.a, data.b))
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
                .Arrange()
                .With(new {a = "a", b = "b", expect = "a,b"})
                .Act(data => string.Join(",", data.a, data.b))
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
                .Arrange()
                .With(new {index = 1, value = 2})
                .Act(data => actStore[data.index] += data.value)
                .Assert((result, data) => store++);

            TestUtil.RunAll(spec);
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
                .Arrange()
                .With(new {index = 1, value = 2})
                .With(new {index = 2, value = 3})
                .Act(data => actStore[data.index] += data.value)
                .Assert((result, data) => store[0]++)
                .Assert((result, data) => store[1]++)
                .Assert("Named", (result, data) => store[2]++)
                .Assert("Named 2", (result, data) => store[3]++);

            TestUtil.RunAll(spec);

            const int expectedActInvokes = 4;
            const int expectedAssertInvokes = 2;
            Assert.AreEqual(2*expectedActInvokes, actStore[1]);
            Assert.AreEqual(3*expectedActInvokes, actStore[2]);
            Assert.AreEqual(5*expectedActInvokes, actStore.Sum());
            Assert.IsTrue(store.All(s => s == expectedAssertInvokes));
        }

        [Test]
        public void reuse_of_post_act_should_give_two_separately_runnable_tests()
        {
            var actInvokes = 0;
            var store = new int[2];
            var builder = "test"
                .Arrange()
                .With(new {index = 1, value = 2})
                .With(new {index = 2, value = 3})
                .Act(data => actInvokes++);

            ISpecification spec1 = builder
                .Assert((result, data) => store[0]++);

            ISpecification spec2 = builder
                .Assert((result, data) => store[1]++);

            TestUtil.RunAll(spec1);

            Assert.AreEqual(2, store[0]);
            Assert.AreEqual(0, store[1]);
            Assert.AreEqual(2, actInvokes);

            TestUtil.RunAll(spec2);

            Assert.AreEqual(2, store[0]);
            Assert.AreEqual(2, store[1]);
            Assert.AreEqual(4, actInvokes);
        }

        [Test]
        public void post_with_act_is_not_ISpecification_without_one_assert()
        {
            var builder = "test"
                .Arrange()
                .With(new {index = 1})
                .Act(data => data.index);

            Assert.IsNotInstanceOf(typeof (ISpecification), builder);
        }
    }
}
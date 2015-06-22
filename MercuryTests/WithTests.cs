using System.Linq;
using Mercury;
using NUnit.Framework;

namespace MercuryTests
{
    [TestFixture]
    public sealed class WithTests
    {
        [Test]
        public void Expect_one_test_from_one_with_and_assert()
        {
            ISpecification spec = "With example"
                .Arrange()
                .With(new {a = 1})
                .Assert((sut, d) => Assert.AreEqual(1, d.a));

            var tests = spec.EmitAllRunnableTests();

            Assert.AreEqual(1, tests.Count());
        }

        [Test]
        public void Expect_two_tests_from_two_withs_and_one_assert()
        {
            ISpecification spec = "With example"
                .Arrange()
                .With(new {a = 1})
                .With(new {a = 2})
                .Assert((sut, d) => Assert.AreEqual(1, d.a));

            var tests = spec.EmitAllRunnableTests();

            Assert.AreEqual(2, tests.Count());
        }

        [Test]
        public void Expect_four_tests_from_two_withs_and_two_asserts()
        {
            ISpecification spec = "With example"
                .Arrange()
                .With(new {a = 1})
                .With(new {a = 2})
                .Assert((sut, d) => Assert.AreEqual(1, d.a))
                .Assert((sut, d) => Assert.AreEqual(1, d.a));

            var tests = spec.EmitAllRunnableTests();

            Assert.AreEqual(4, tests.Count());
        }

        [Test]
        public void Expect_six_tests_from_three_withs_and_two_asserts()
        {
            ISpecification spec = "With example"
                .Arrange()
                .With(new {a = 1})
                .With(new {a = 2})
                .With(new {a = 3})
                .Assert((sut, d) => Assert.AreEqual(1, d.a))
                .Assert((sut, d) => Assert.AreEqual(1, d.a));

            var tests = spec.EmitAllRunnableTests();

            Assert.AreEqual(6, tests.Count());
        }

        [Test]
        public void Each_data_assert_pair_is_executed()
        {
            var array = new int[6];
            ISpecification spec = "With example"
                .Arrange()
                .With(new {a = 1})
                .With(new {a = 2})
                .With(new {a = 3})
                .Assert((sut, d) => array[d.a - 1]++)
                .Assert((sut, d) => array[d.a + 2]++);

            foreach (var test in spec.EmitAllRunnableTests())
                test.Run();

            Assert.AreEqual(6, array.Sum());
        }

        [Test]
        public void Each_data_assert_pair_and_their_act_is_executed()
        {
            var array = new int[12];
            ISpecification spec = "With example"
                .Arrange()
                .With(new {a = -1})
                .With(new {a = -2})
                .With(new {a = -3})
                .With(new {a = -4})
                .Act((sut, d) => -d.a)
                .Assert((s, d) => array[s - 1]++)
                .Assert((s, d) => array[s + 2]++)
                .Assert((s, d) => array[s + 5]++);

            foreach (var test in spec.EmitAllRunnableTests())
                test.Run();

            Assert.AreEqual(12, array.Sum());
        }
    }
}
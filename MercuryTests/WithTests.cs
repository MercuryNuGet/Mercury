using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Mercury;

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
                .With(new { a = 1 })
                .Assert((sut, d) => Assert.AreEqual(1, d.a));

            var tests = spec.EmitAllRunnableTests();

            Assert.AreEqual(1, tests.Count());
        }

        [Test]
        public void Expect_two_tests_from_two_withs_and_one_assert()
        {
            ISpecification spec = "With example"
                .Arrange()
                .With(new { a = 1 })
                .With(new { a = 2 })
                .Assert((sut, d) => Assert.AreEqual(1, d.a));

            var tests = spec.EmitAllRunnableTests();

            Assert.AreEqual(2, tests.Count());
        }

        [Test]
        public void Expect_four_tests_from_two_withs_and_two_asserts()
        {
            ISpecification spec = "With example"
                .Arrange()
                .With(new { a = 1 })
                .With(new { a = 2 })
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
                .With(new { a = 1 })
                .With(new { a = 2 })
                .With(new { a = 3 })
                .Assert((sut, d) => Assert.AreEqual(1, d.a))
                .Assert((sut, d) => Assert.AreEqual(1, d.a));

            var tests = spec.EmitAllRunnableTests();

            Assert.AreEqual(6, tests.Count());
        }
    }
}

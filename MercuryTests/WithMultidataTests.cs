//Generated from Template
using Mercury;
using System.Linq;
using NUnit.Framework;

namespace MercuryTests {

    /// <summary>
    /// Tests for With multidata with 1 elements
    /// </summary>
    [TestFixture]
    public sealed class WithMultidata1 {

	    [Test]
		public void Can_inject_name(){
		     var spec = "Using #1 #2 #3 #4 #5 to inject data".Arrange(() => ">")
                .With("SomeString")
				.Act((sut, data1) => sut + data1)
                .Assert((actual, data) => Assert.Pass());
             Assert.AreEqual("Using SomeString #2 #3 #4 #5 to inject data", spec.EmitAllRunnableTests().Single().Name);
		}

		[Test]
        public void Can_fail()
        {
             var spec = "Failing".Arrange(() => ">")
                .With("SomeString")
				.Act((sut, data1) => sut + data1)
                .Assert((actual, data) => Assert.Fail());
            Assert.Throws<AssertionException>(() => TestUtil.RunAll(spec));
        }

	}
	
    /// <summary>
    /// Tests for With multidata with 2 elements
    /// </summary>
    [TestFixture]
    public sealed class WithMultidata2 {

	    [Test]
		public void Can_inject_name(){
		     var spec = "Using #1 #2 #3 #4 #5 to inject data".Arrange(() => ">")
                .With("SomeString", 123)
				.Act((sut, data1, data2) => sut + data1 + data2)
                .Assert((actual, data) => Assert.Pass());
             Assert.AreEqual("Using SomeString 123 #3 #4 #5 to inject data", spec.EmitAllRunnableTests().Single().Name);
		}

		[Test]
        public void Can_fail()
        {
             var spec = "Failing".Arrange(() => ">")
                .With("SomeString", 123)
				.Act((sut, data1, data2) => sut + data1 + data2)
                .Assert((actual, data) => Assert.Fail());
            Assert.Throws<AssertionException>(() => TestUtil.RunAll(spec));
        }

		[Test]
        public void Assert_expected_extension()
        {
            var spec = "Assert Expected".Arrange(() => ">")
               .With("SomeString", ">SomeString")
               .Act((sut, data1, expect) => sut + data1)
               .AssertEqualsExpected();
            TestUtil.RunAll(spec);
        }

		[Test]
        public void Assert_expected_extension_multi_with()
        {
            var spec = "Assert Expected".Arrange(() => ">")
               .With("SomeString", ">SomeString")
			   .With("SomeString", ">SomeString")
               .Act((sut, data1, expect) => sut + data1)
               .AssertEqualsExpected();
            TestUtil.RunAll(spec);
        }

		[Test]
        public void Assert_expected_extension_name()
        {
            var name = "Assert concatenation of data".Arrange(() => ">")
               .With("SomeString", ">SomeString")
               .Act((sut, data1, expect) => sut + data1)
               .AssertEqualsExpected().EmitAllRunnableTests().Single().Name;
            Assert.AreEqual("Assert concatenation of data is equal to >SomeString", name);
        }

		[Test]
        public void Assert_expected_extension_can_fail()
        {
            var spec = "Assert Expected Failing".Arrange(() => ">")
               .With("SomeString", "expected")
               .Act((sut, data1, expect) => sut + data1)
               .AssertEqualsExpected();
            Assert.Throws<AssertionException>(() => TestUtil.RunAll(spec));
        }
	}
	
    /// <summary>
    /// Tests for With multidata with 3 elements
    /// </summary>
    [TestFixture]
    public sealed class WithMultidata3 {

	    [Test]
		public void Can_inject_name(){
		     var spec = "Using #1 #2 #3 #4 #5 to inject data".Arrange(() => ">")
                .With("SomeString", 123, 456)
				.Act((sut, data1, data2, data3) => sut + data1 + data2 + data3)
                .Assert((actual, data) => Assert.Pass());
             Assert.AreEqual("Using SomeString 123 456 #4 #5 to inject data", spec.EmitAllRunnableTests().Single().Name);
		}

		[Test]
        public void Can_fail()
        {
             var spec = "Failing".Arrange(() => ">")
                .With("SomeString", 123, 456)
				.Act((sut, data1, data2, data3) => sut + data1 + data2 + data3)
                .Assert((actual, data) => Assert.Fail());
            Assert.Throws<AssertionException>(() => TestUtil.RunAll(spec));
        }

		[Test]
        public void Assert_expected_extension()
        {
            var spec = "Assert Expected".Arrange(() => ">")
               .With("SomeString", 123, ">SomeString123")
               .Act((sut, data1, data2, expect) => sut + data1 + data2)
               .AssertEqualsExpected();
            TestUtil.RunAll(spec);
        }

		[Test]
        public void Assert_expected_extension_multi_with()
        {
            var spec = "Assert Expected".Arrange(() => ">")
               .With("SomeString", 123, ">SomeString123")
			   .With("SomeString", 123, ">SomeString123")
               .Act((sut, data1, data2, expect) => sut + data1 + data2)
               .AssertEqualsExpected();
            TestUtil.RunAll(spec);
        }

		[Test]
        public void Assert_expected_extension_name()
        {
            var name = "Assert concatenation of data".Arrange(() => ">")
               .With("SomeString", 123, ">SomeString123")
               .Act((sut, data1, data2, expect) => sut + data1 + data2)
               .AssertEqualsExpected().EmitAllRunnableTests().Single().Name;
            Assert.AreEqual("Assert concatenation of data is equal to >SomeString123", name);
        }

		[Test]
        public void Assert_expected_extension_can_fail()
        {
            var spec = "Assert Expected Failing".Arrange(() => ">")
               .With("SomeString", 123, "expected")
               .Act((sut, data1, data2, expect) => sut + data1 + data2)
               .AssertEqualsExpected();
            Assert.Throws<AssertionException>(() => TestUtil.RunAll(spec));
        }
	}
	
    /// <summary>
    /// Tests for With multidata with 4 elements
    /// </summary>
    [TestFixture]
    public sealed class WithMultidata4 {

	    [Test]
		public void Can_inject_name(){
		     var spec = "Using #1 #2 #3 #4 #5 to inject data".Arrange(() => ">")
                .With("SomeString", 123, 456, 8.2m)
				.Act((sut, data1, data2, data3, data4) => sut + data1 + data2 + data3 + data4)
                .Assert((actual, data) => Assert.Pass());
             Assert.AreEqual("Using SomeString 123 456 8.2 #5 to inject data", spec.EmitAllRunnableTests().Single().Name);
		}

		[Test]
        public void Can_fail()
        {
             var spec = "Failing".Arrange(() => ">")
                .With("SomeString", 123, 456, 8.2m)
				.Act((sut, data1, data2, data3, data4) => sut + data1 + data2 + data3 + data4)
                .Assert((actual, data) => Assert.Fail());
            Assert.Throws<AssertionException>(() => TestUtil.RunAll(spec));
        }

		[Test]
        public void Assert_expected_extension()
        {
            var spec = "Assert Expected".Arrange(() => ">")
               .With("SomeString", 123, 456, ">SomeString123456")
               .Act((sut, data1, data2, data3, expect) => sut + data1 + data2 + data3)
               .AssertEqualsExpected();
            TestUtil.RunAll(spec);
        }

		[Test]
        public void Assert_expected_extension_multi_with()
        {
            var spec = "Assert Expected".Arrange(() => ">")
               .With("SomeString", 123, 456, ">SomeString123456")
			   .With("SomeString", 123, 456, ">SomeString123456")
               .Act((sut, data1, data2, data3, expect) => sut + data1 + data2 + data3)
               .AssertEqualsExpected();
            TestUtil.RunAll(spec);
        }

		[Test]
        public void Assert_expected_extension_name()
        {
            var name = "Assert concatenation of data".Arrange(() => ">")
               .With("SomeString", 123, 456, ">SomeString123456")
               .Act((sut, data1, data2, data3, expect) => sut + data1 + data2 + data3)
               .AssertEqualsExpected().EmitAllRunnableTests().Single().Name;
            Assert.AreEqual("Assert concatenation of data is equal to >SomeString123456", name);
        }

		[Test]
        public void Assert_expected_extension_can_fail()
        {
            var spec = "Assert Expected Failing".Arrange(() => ">")
               .With("SomeString", 123, 456, "expected")
               .Act((sut, data1, data2, data3, expect) => sut + data1 + data2 + data3)
               .AssertEqualsExpected();
            Assert.Throws<AssertionException>(() => TestUtil.RunAll(spec));
        }
	}
	
    /// <summary>
    /// Tests for With multidata with 5 elements
    /// </summary>
    [TestFixture]
    public sealed class WithMultidata5 {

	    [Test]
		public void Can_inject_name(){
		     var spec = "Using #1 #2 #3 #4 #5 to inject data".Arrange(() => ">")
                .With("SomeString", 123, 456, 8.2m, 76f)
				.Act((sut, data1, data2, data3, data4, data5) => sut + data1 + data2 + data3 + data4 + data5)
                .Assert((actual, data) => Assert.Pass());
             Assert.AreEqual("Using SomeString 123 456 8.2 76 to inject data", spec.EmitAllRunnableTests().Single().Name);
		}

		[Test]
        public void Can_fail()
        {
             var spec = "Failing".Arrange(() => ">")
                .With("SomeString", 123, 456, 8.2m, 76f)
				.Act((sut, data1, data2, data3, data4, data5) => sut + data1 + data2 + data3 + data4 + data5)
                .Assert((actual, data) => Assert.Fail());
            Assert.Throws<AssertionException>(() => TestUtil.RunAll(spec));
        }

		[Test]
        public void Assert_expected_extension()
        {
            var spec = "Assert Expected".Arrange(() => ">")
               .With("SomeString", 123, 456, 8.2m, ">SomeString1234568.2")
               .Act((sut, data1, data2, data3, data4, expect) => sut + data1 + data2 + data3 + data4)
               .AssertEqualsExpected();
            TestUtil.RunAll(spec);
        }

		[Test]
        public void Assert_expected_extension_multi_with()
        {
            var spec = "Assert Expected".Arrange(() => ">")
               .With("SomeString", 123, 456, 8.2m, ">SomeString1234568.2")
			   .With("SomeString", 123, 456, 8.2m, ">SomeString1234568.2")
               .Act((sut, data1, data2, data3, data4, expect) => sut + data1 + data2 + data3 + data4)
               .AssertEqualsExpected();
            TestUtil.RunAll(spec);
        }

		[Test]
        public void Assert_expected_extension_name()
        {
            var name = "Assert concatenation of data".Arrange(() => ">")
               .With("SomeString", 123, 456, 8.2m, ">SomeString1234568.2")
               .Act((sut, data1, data2, data3, data4, expect) => sut + data1 + data2 + data3 + data4)
               .AssertEqualsExpected().EmitAllRunnableTests().Single().Name;
            Assert.AreEqual("Assert concatenation of data is equal to >SomeString1234568.2", name);
        }

		[Test]
        public void Assert_expected_extension_can_fail()
        {
            var spec = "Assert Expected Failing".Arrange(() => ">")
               .With("SomeString", 123, 456, 8.2m, "expected")
               .Act((sut, data1, data2, data3, data4, expect) => sut + data1 + data2 + data3 + data4)
               .AssertEqualsExpected();
            Assert.Throws<AssertionException>(() => TestUtil.RunAll(spec));
        }
	}
	

}
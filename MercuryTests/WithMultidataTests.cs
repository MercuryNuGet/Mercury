//Generated from Template
using Mercury;
using System.Linq;
using NUnit.Framework;

namespace MercuryTests {


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
	}

	

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
	}

	

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
	}

	

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
	}

	

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
	}

	

}
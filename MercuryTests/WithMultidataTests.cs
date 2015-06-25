//Generated from Template
using Mercury;
using System.Linq;
using NUnit.Framework;

namespace MercuryTests {


    [TestFixture]
    public sealed class WithMultidata1 {

	    [Test]
		public void Can_inject_name(){
		     var spec = "Using #1 to inject data".Arrange(() => ">")
                .With("SomeData")
                .Act((sut, data) => sut + data)
                .Assert((actual, data) => Assert.Pass());
            spec.EmitAllRunnableTests().Single();
		}
	}

	

    [TestFixture]
    public sealed class WithMultidata2 {

	    [Test]
		public void Can_inject_name(){
		     var spec = "Using #1 to inject data".Arrange(() => ">")
                .With("SomeData")
                .Act((sut, data) => sut + data)
                .Assert((actual, data) => Assert.Pass());
            spec.EmitAllRunnableTests().Single();
		}
	}

	

    [TestFixture]
    public sealed class WithMultidata3 {

	    [Test]
		public void Can_inject_name(){
		     var spec = "Using #1 to inject data".Arrange(() => ">")
                .With("SomeData")
                .Act((sut, data) => sut + data)
                .Assert((actual, data) => Assert.Pass());
            spec.EmitAllRunnableTests().Single();
		}
	}

	

    [TestFixture]
    public sealed class WithMultidata4 {

	    [Test]
		public void Can_inject_name(){
		     var spec = "Using #1 to inject data".Arrange(() => ">")
                .With("SomeData")
                .Act((sut, data) => sut + data)
                .Assert((actual, data) => Assert.Pass());
            spec.EmitAllRunnableTests().Single();
		}
	}

	

    [TestFixture]
    public sealed class WithMultidata5 {

	    [Test]
		public void Can_inject_name(){
		     var spec = "Using #1 to inject data".Arrange(() => ">")
                .With("SomeData")
                .Act((sut, data) => sut + data)
                .Assert((actual, data) => Assert.Pass());
            spec.EmitAllRunnableTests().Single();
		}
	}

	

}
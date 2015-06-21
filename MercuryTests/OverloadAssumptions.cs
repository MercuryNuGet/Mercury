using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Dynamic;

namespace MercuryTests
{
    [TestFixture]
    public sealed class OverloadAssumptions
    {
        [Test]
        public void Invoke()
        {
           var t = SomeMethod(new { a = 1 });
           Assert.AreEqual(1, t.P.a);
           t = SomeMethod(new { a = 2 });
           Assert.AreEqual(2, t.P.a);
        }

        private IMyTest<T> SomeMethod<T>(T p)
        {
            return new Container<T>(p);
        }

    }

    interface IMyTest<T>
    {
        T P { get; }
    }

    class Container<T> : IMyTest<T>
    {
        private T p;

        public Container(T p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }

        public T P { get { return p; } }
    }
}

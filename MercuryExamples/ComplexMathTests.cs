using System;
using Mercury;
using NUnit.Framework;

namespace MercuryExamples
{
    internal class ComplexMathTests : Specification
    {
        protected override ISpecification[] TestCases()
        {
            return new ISpecification[]
            {
                "Can to string #r,#i and get \"#expect\""
                    .ArrangeNull()
                    .With(new {r = 0, i = 0, expect = "0 + 0i"})
                    .With(new {r = 1, i = 2, expect = "1 + 2i"})
                    .Act((sut, data) => new Complex(data.r, data.i).ToString())
                    .Assert((s, o) => Assert.AreEqual(o.expect, s)),
                "Equality of #lhs and #rhs:"
                    .ArrangeNull()
                    .With(new {lhs = new Complex(0, 0), rhs = new Complex(0, 0)})
                    .With(new {lhs = new Complex(1, 0), rhs = new Complex(1, 0)})
                    .With(new {lhs = new Complex(0, 1), rhs = new Complex(0, 1)})
                    .With(new {lhs = new Complex(1, 2), rhs = new Complex(1, 2)})
                    .Assert("equal", (o, d) => Assert.AreEqual(d.lhs, d.rhs))
                    .Assert("reflexive equal", (o, d) => Assert.AreEqual(d.rhs, d.lhs))
                    .Assert("hashCode equal", (o, d) => Assert.AreEqual(d.lhs.GetHashCode(), d.rhs.GetHashCode())),
                "Inequality of #lhs and #rhs:"
                    .ArrangeNull()
                    .With(new {lhs = new Complex(0, 0), rhs = (object) new Complex(1, 0)})
                    .With(new {lhs = new Complex(0, 0), rhs = (object) new Complex(0, 1)})
                    .With(new {lhs = new Complex(1, 0), rhs = (object) new Complex(0, 0)})
                    .With(new {lhs = new Complex(0, 1), rhs = (object) new Complex(0, 0)})
                    .With(new {lhs = new Complex(0, 1), rhs = new object()})
                    .Assert("not equal", (o, d) => Assert.AreNotEqual(d.lhs, d.rhs))
                    .Assert("hashCode not equal", (o, d) => Assert.AreNotEqual(d.lhs.GetHashCode(), d.rhs.GetHashCode())),
                "Can add #a to #b and should get #expect"
                    .Arrange()
                    .With(new {a = new Complex(0, 0), b = new Complex(0, 0), expect = new Complex(0, 0)})
                    .With(new {a = new Complex(1, 0), b = new Complex(0, 0), expect = new Complex(1, 0)})
                    .With(new {a = new Complex(0, 0), b = new Complex(1, 0), expect = new Complex(1, 0)})
                    .With(new {a = new Complex(0, 1), b = new Complex(0, 0), expect = new Complex(0, 1)})
                    .With(new {a = new Complex(0, 0), b = new Complex(0, 1), expect = new Complex(0, 1)})
                    .With(new {a = new Complex(1, 2), b = new Complex(4, 8), expect = new Complex(5, 10)})
                    .Act(d => d.a + d.b)
                    .Assert((actual, o1) => Assert.AreEqual(o1.expect, actual))
            };
        }
    }

    public class Complex
    {
        private readonly double _real;
        private readonly double _imaginary;

        public Complex(double real, double imaginary)
        {
            _real = real;
            _imaginary = imaginary;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Complex;
            if (other == null) return false;
            return other._real.Equals(_real) && other._imaginary.Equals(_imaginary);
        }

        public override int GetHashCode()
        {
            return _real.GetHashCode() ^ _imaginary.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("{0} + {1}i", _real, _imaginary);
        }

        public static Complex operator +(Complex c1, Complex c2)
        {
            return new Complex(c1._real + c2._real, c1._imaginary + c2._imaginary);
        }
    }
}
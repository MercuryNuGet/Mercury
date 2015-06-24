using System;
using System.Collections.Generic;

namespace Mercury
{
    internal sealed class SingleRunnableTestCase<TResult> : ISingleRunnableTestCase<TResult>,
        ISpecification
    {
        private readonly string _str;
        private readonly Func<TResult> _test;

        public SingleRunnableTestCase(string str, Func<TResult> test)
        {
            _str = str;
            _test = test;
        }

        public string Name
        {
            get { return _str; }
        }

        public Func<TResult> TestMethodWithResult { get { return _test; } }

        public Action TestMethod
        {
            get { return () => _test(); }
        }

        public void Run()
        {
            _test();
        }

        public override string ToString()
        {
            return _str;
        }

        public IEnumerable<ISingleRunnableTestCase> EmitAllRunnableTests()
        {
            return new ISingleRunnableTestCase[] {this};
        }
    }
}
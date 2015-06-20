using System;
using System.Collections.Generic;

namespace Mercury
{
    internal sealed class SingleRunnableTestCase : ISingleRunnableTestCase, ISpecification
    {
        private readonly string _str;
        private readonly Action _test;

        public SingleRunnableTestCase(string str, Action test)
        {
            _str = str;
            _test = test;
        }

        public string Name
        {
            get { return _str; }
        }

        public Action TestMethod
        {
            get { return _test; }
        }

        public void Run()
        {
            _test();
        }

        public override string ToString()
        {
            return _str;
        }

        public IEnumerable<ISingleRunnableTestCase> GetAll()
        {
            return new ISingleRunnableTestCase[] {this};
        }
    }
}
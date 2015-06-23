using System;
using System.Collections.Generic;

namespace Mercury.AssertBuilder
{
    internal sealed class TestCaseAccumulator
    {
        private readonly List<ISingleRunnableTestCase> _builtTests = new List<ISingleRunnableTestCase>();

        public void AddSingleTest(string testCaseName, Action testAction)
        {
            _builtTests.Add(new SingleRunnableTestCase(testCaseName, testAction));
        }

        public IEnumerable<ISingleRunnableTestCase> EmitAllRunnableTests()
        {
            return _builtTests;
        }

        public void Add(TestCaseAccumulator accumulator)
        {
            _builtTests.AddRange(accumulator._builtTests);
        }
    }
}
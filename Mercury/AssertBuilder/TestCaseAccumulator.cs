using System;
using System.Collections.Generic;
using System.Linq;

namespace Mercury.AssertBuilder
{
    internal sealed class TestCaseAccumulator<TResult>
    {
        private readonly List<ISingleRunnableTestCase<TResult>> _builtTests = new List<ISingleRunnableTestCase<TResult>>();

        public void AddSingleTest(string testCaseName, Func<TResult> testAction)
        {
            _builtTests.Add(new SingleRunnableTestCase<TResult>(testCaseName, testAction));
        }

        public IEnumerable<ISingleRunnableTestCase> EmitAllRunnableTests()
        {
            return _builtTests;
        }

        public void Add(TestCaseAccumulator<TResult> accumulator)
        {
            _builtTests.AddRange(accumulator._builtTests);
        }

        public void Add(IEnumerable<ISpecification> specs)
        {
            foreach (var specification in specs)
            {
                Add(specification);
            }
        }

        private void Add(ISpecification specification)
        {
                
        }
    }
}
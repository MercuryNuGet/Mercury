using System;
using System.Collections.Generic;

namespace Mercury.AssertBuilder
{
    internal sealed class AssertBuilder<TResult> : IPostAssertCaseBuilder<TResult>
    {
        private readonly ISuite _suite;
        private readonly Func<TResult> _actFunc;
        private readonly TestCaseAccumulator _accumulator = new TestCaseAccumulator();

        public AssertBuilder(ISuite suite, Func<TResult> actFunc)
        {
            _suite = suite;
            _actFunc = actFunc;
        }

        public IPostAssertCaseBuilder<TResult> Assert(Action<TResult> assertAction)
        {
            _accumulator.AddSingleTest(_suite.SuiteName, () => assertAction(_actFunc()));
            return this;
        }

        public IPostAssertCaseBuilder<TResult> Assert(string assertionTestCaseName, Action<TResult> assertAction)
        {
            _accumulator.AddSingleTest(_suite.SuiteName + " " + assertionTestCaseName, () => assertAction(_actFunc()));
            return this;
        }

        public IEnumerable<ISingleRunnableTestCase> EmitAllRunnableTests()
        {
            return _accumulator.EmitAllRunnableTests();
        }
    }
}
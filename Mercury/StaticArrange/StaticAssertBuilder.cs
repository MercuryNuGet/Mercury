using System;
using System.Collections.Generic;

namespace Mercury.StaticArrange
{
    internal sealed class StaticAssertBuilder<TResult> : IStaticAssertCaseBuilder<TResult>
    {
        private readonly string _testName;
        private readonly Func<TResult> _actFunc;
        private readonly TestCaseAccumulator _accumulator = new TestCaseAccumulator();

        public StaticAssertBuilder(string testName, Func<TResult> actFunc)
        {
            _testName = testName;
            _actFunc = actFunc;
        }

        public IStaticAssertCaseBuilder<TResult> Assert(Action<TResult> assertAction)
        {
            _accumulator.AddSingleTest(_testName, () => assertAction(_actFunc()));
            return this;
        }

        public IStaticAssertCaseBuilder<TResult> Assert(string assertionTestCaseName, Action<TResult> assertAction)
        {
            _accumulator.AddSingleTest(_testName + " " + assertionTestCaseName, () => assertAction(_actFunc()));
            return this;
        }

        public IEnumerable<ISingleRunnableTestCase> EmitAllRunnableTests()
        {
            return _accumulator.EmitAllRunnableTests();
        }
    }
}
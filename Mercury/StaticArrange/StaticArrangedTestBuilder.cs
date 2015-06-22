using System;

namespace Mercury.StaticArrange
{
    internal sealed class StaticArrangedTestBuilder : IStaticArranged
    {
        private readonly string _testName;

        public StaticArrangedTestBuilder(string testName)
        {
            _testName = testName;
        }

        public IStaticAssertCaseBuilder<TResult> Act<TResult>(Func<TResult> actFunc)
        {
            return new StaticAssertBuilder<TResult>(_testName, actFunc);
        }
    }
}
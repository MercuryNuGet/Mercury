using System;

namespace Mercury.AssertBuilder
{
    internal sealed class StaticPreAssertBuilder<TResult> : IAssertCaseBuilder<TResult>
    {
        private readonly ISuite _suite;
        private readonly Func<TResult> _actFunc;

        public StaticPreAssertBuilder(ISuite suite, Func<TResult> actFunc)
        {
            _suite = suite;
            _actFunc = actFunc;
        }

        public IPostAssertCaseBuilder<TResult> Assert(Action<TResult> assertAction)
        {
            return new AssertBuilder<TResult>(_suite, _actFunc).Assert(assertAction);
        }

        public IPostAssertCaseBuilder<TResult> Assert(string assertionTestCaseName, Action<TResult> assertAction)
        {
            return new AssertBuilder<TResult>(_suite, _actFunc).Assert(assertionTestCaseName, assertAction);
        }
    }
}
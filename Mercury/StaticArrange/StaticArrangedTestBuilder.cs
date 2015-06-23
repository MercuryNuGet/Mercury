using System;
using Mercury.AssertBuilder;

namespace Mercury.StaticArrange
{
    internal sealed class StaticArrangedTestBuilder : IStaticArranged, ISuite
    {
        private readonly string _testName;

        public StaticArrangedTestBuilder(string testName)
        {
            _testName = testName;
        }

        public IAssertCaseBuilder<TResult> Act<TResult>(Func<TResult> actFunc)
        {
            return new StaticPreAssertBuilder<TResult>(this, actFunc);
        }

        public IStaticArrangedWithData<TData> With<TData>(TData data)
        {
            return new StaticArrangedDataBuilder<TData>(this).With(data);
        }

        public string SuiteName
        {
            get { return _testName; }
        }
    }
}
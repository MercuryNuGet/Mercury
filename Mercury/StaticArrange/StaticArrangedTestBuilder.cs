using System;

namespace Mercury.StaticArrange
{
    internal sealed class StaticArrangedTestBuilder : IStaticArranged, ISuite
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
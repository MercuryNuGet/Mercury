using System;
using System.Collections.Generic;

namespace Mercury
{
    internal sealed class DataBuilder<TSut, TData> : IDataArrangedTest<TSut, TData>
    {
        private readonly List<TData> _data = new List<TData>();
        private readonly TestCaseBuilder<TSut> _testCaseBuilder;

        public DataBuilder(TestCaseBuilder<TSut> testCaseBuilder)
        {
            _testCaseBuilder = testCaseBuilder;
        }

        public IDataArrangedTest<TSut, TData> With(TData data)
        {
            _data.Add(data);
            return this;
        }

        public IDataAssertCaseBuilder<TResult, TData> Act<TResult>(Func<TSut, TData, TResult> actFunc)
        {
            return new DataAssertBuilder<TSut, TData, TResult>(_testCaseBuilder, actFunc, _data);
        }

        public IDataAssertCaseBuilder<TSut, TData> Assert(Action<TSut, TData> assertMethod)
        {
            return Act((sut, data) => sut).Assert(assertMethod);
        }

        public IDataAssertCaseBuilder<TSut, TData> Assert(string assertionTestCaseName, Action<TSut, TData> assertMethod)
        {
            return Act((sut, data) => sut).Assert(assertionTestCaseName, assertMethod);
        }

        public IEnumerable<ISingleRunnableTestCase> EmitAllRunnableTests()
        {
            return _testCaseBuilder.EmitAllRunnableTests();
        }
    }
}

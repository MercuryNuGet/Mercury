using System;
using System.Collections.Generic;

namespace Mercury
{
    internal sealed class DataBuilder<TSut, TData> : IDataArrangedTest<TSut, TData>
    {
        private readonly List<TData> _data = new List<TData>();
        private readonly ITestCaseBuilder<TSut> _testCaseBuilder;

        public DataBuilder(ITestCaseBuilder<TSut> testCaseBuilder)
        {
            _testCaseBuilder = testCaseBuilder;
        }

        public IDataArrangedTest<TSut, TData> With(TData data)
        {
            _data.Add(data);
            return this;
        }

        public IAssertWithDataCaseBuilder<TResult, TData> Act<TResult>(Func<TSut, TData, TResult> actFunc)
        {
            return new DataAssertBuilder<TSut, TData, TResult>(_testCaseBuilder, actFunc, _data);
        }

        public IAssertWithDataCaseBuilder<TSut, TData> Assert(Action<TSut, TData> assertMethod)
        {
            return Act((sut, data) => sut).Assert(assertMethod);
        }

        public IAssertWithDataCaseBuilder<TSut, TData> Assert(string assertionTestCaseName, Action<TSut, TData> assertMethod)
        {
            return Act((sut, data) => sut).Assert(assertionTestCaseName, assertMethod);
        }

        public IEnumerable<ISingleRunnableTestCase> EmitAllRunnableTests()
        {
            return _testCaseBuilder.EmitAllRunnableTests();
        }
    }
}
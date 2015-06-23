using System;

namespace Mercury.AssertBuilder
{
    internal sealed class DataPreAssertBuilder<TSut, TData> : IAssertWithDataCaseBuilder<TSut, TData>
    {
        private readonly Func<TData, TSut> _actFunc;
        private readonly IDataSuite<TData> _dataSuite;

        public DataPreAssertBuilder(Func<TData, TSut> actFunc, IDataSuite<TData> dataSuite)
        {
            _actFunc = actFunc;
            _dataSuite = dataSuite;
        }

        public IPostAssertWithDataCaseBuilder<TSut, TData> Assert(Action<TSut, TData> assertAction)
        {
            return new DataAssertBuilder<TSut, TData>(_actFunc, _dataSuite).Assert(assertAction);
        }

        public IPostAssertWithDataCaseBuilder<TSut, TData> Assert(string assertionTestCaseName,
            Action<TSut, TData> assertAction)
        {
            return new DataAssertBuilder<TSut, TData>(_actFunc, _dataSuite).Assert(assertionTestCaseName,
                assertAction);
        }
    }
}
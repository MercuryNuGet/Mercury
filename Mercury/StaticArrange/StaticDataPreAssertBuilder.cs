using System;
using System.Collections.Generic;

namespace Mercury.StaticArrange
{
    internal sealed class StaticDataPreAssertBuilder<TSut, TData> : IAssertWithDataCaseBuilder<TSut, TData>
    {
        private readonly Func<TData, TSut> _actFunc;
        private readonly IDataSuite<TData> _dataSuite;

        public StaticDataPreAssertBuilder(Func<TData, TSut> actFunc, IDataSuite<TData> dataSuite)
        {
            _actFunc = actFunc;
            _dataSuite = dataSuite;
        }

        public IEnumerable<ISingleRunnableTestCase> EmitAllRunnableTests()
        {
            throw new NotImplementedException();
        }

        public IAssertWithDataCaseBuilder<TSut, TData> Assert(Action<TSut, TData> assertAction)
        {
            return new StaticDataAssertBuilder<TSut, TData>(_actFunc, _dataSuite).Assert(assertAction);
        }

        public IAssertWithDataCaseBuilder<TSut, TData> Assert(string assertionTestCaseName,
            Action<TSut, TData> assertAction)
        {
            return new StaticDataAssertBuilder<TSut, TData>(_actFunc, _dataSuite).Assert(assertionTestCaseName,
                assertAction);
        }
    }
}
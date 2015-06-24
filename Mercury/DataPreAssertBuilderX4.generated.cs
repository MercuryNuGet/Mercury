// Code generated by a template
using System;
using Mercury.AssertBuilder;
using System.Collections.Generic;

namespace Mercury.AssertBuilder
{
	internal sealed class DataPreAssertBuilder<TSut, TData1, TData2, TData3, TData4> : IAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3, TData4>
    {
        private readonly Func<TData1, TData2, TData3, TData4, TSut> _actFunc;
        private readonly IDataSuite<Tuple<TData1, TData2, TData3, TData4>> _dataSuite;

        public DataPreAssertBuilder(Func<TData1, TData2, TData3, TData4, TSut> actFunc,
                           IDataSuite<Tuple<TData1, TData2, TData3, TData4>> dataSuite)
        {
            _actFunc = actFunc;
            _dataSuite = dataSuite;
        }

        public IPostAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3, TData4> Assert(Action<TSut, TData1, TData2, TData3, TData4> assertAction)
        {
            return new DataAssertBuilder<TSut, TData1, TData2, TData3, TData4>(_actFunc, _dataSuite).Assert(assertAction);
        }

        public IPostAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3, TData4> Assert(string assertionTestCaseName,
            Action<TSut, TData1, TData2, TData3, TData4> assertAction)
        {
            return new DataAssertBuilder<TSut, TData1, TData2, TData3, TData4>(_actFunc, _dataSuite).Assert(assertionTestCaseName,
                assertAction);
        }
    }
}

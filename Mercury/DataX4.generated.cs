// Code generated by a template
using System;
using Mercury.AssertBuilder;
using System.Collections.Generic;

namespace Mercury {

  public interface IArrangedWithData<out TSut, TData1, TData2, TData3, TData4>
  {
     IAssertWithDataCaseBuilder<TPostAct, TData1, TData2, TData3, TData4> Act<TPostAct>(Func<TSut, TData1, TData2, TData3, TData4, TPostAct> actFunc);
     IArrangedWithData<TSut, TData1, TData2, TData3, TData4> With(TData1 data1, TData2 data2, TData3 data3, TData4 data4);
  }

  public interface IAssertWithDataCaseBuilder<out TSut, out TData1, out TData2, out TData3, out TData4>
  {
      IPostAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3, TData4> Assert(Action<TSut, TData1, TData2, TData3, TData4> assertAction);
      IPostAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3, TData4> Assert(string assertionTestCaseName, Action<TSut, TData1, TData2, TData3, TData4> assertAction);
  }

  public interface IPostAssertWithDataCaseBuilder<out TSut, out TData1, out TData2, out TData3, out TData4> : ISpecification,
        IAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3, TData4>
  {
  }

  internal sealed class DataAssertBuilder<TSut, TData1, TData2, TData3, TData4> : IPostAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3, TData4>
  {
        private readonly TestCaseAccumulator _tests = new TestCaseAccumulator();
        private readonly Func<TData1, TData2, TData3, TData4, TSut> _actFunc;
        private readonly IDataSuite<Tuple<TData1, TData2, TData3, TData4>> _dataSuite;

        public DataAssertBuilder(Func<TData1, TData2, TData3, TData4, TSut> actFunc, IDataSuite<Tuple<TData1, TData2, TData3, TData4>> dataSuite)
        {
            _actFunc = actFunc;
            _dataSuite = dataSuite;
        }

        public IPostAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3, TData4> Assert(Action<TSut, TData1, TData2, TData3, TData4> assertMethod)
        {
            InternalAssert(_dataSuite.SuiteName, assertMethod);
            return this;
        }

        public IPostAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3, TData4> Assert(string assertionTestCaseName,
            Action<TSut, TData1, TData2, TData3, TData4> assertMethod)
        {
            InternalAssert(_dataSuite.SuiteName + " " + assertionTestCaseName, assertMethod);
            return this;
        }

        private void InternalAssert(string testName, Action<TSut, TData1, TData2, TData3, TData4> assertMethod)
        {
            foreach (var data in _dataSuite.Data)
            {
                var d = data;
                string inject = NameInjection.Inject(testName, d);
                Action assertTestMethod = () =>
                {
                    TSut acted = _actFunc(d.Item1, d.Item2, d.Item3, d.Item4);
                    assertMethod(acted, d.Item1, d.Item2, d.Item3, d.Item4);
                };
                _tests.AddSingleTest(inject, assertTestMethod);
            }
        }

        public IEnumerable<ISingleRunnableTestCase> EmitAllRunnableTests()
        {
            return _tests.EmitAllRunnableTests();
        }
    }
}

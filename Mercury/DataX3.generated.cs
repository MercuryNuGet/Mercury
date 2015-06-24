// Code generated by a template
using System;
using Mercury.AssertBuilder;
using System.Collections.Generic;

namespace Mercury {

  public interface IArrangedWithData<out TSut, TData1, TData2, TData3>
  {
     IAssertWithDataCaseBuilder<TPostAct, TData1, TData2, TData3> Act<TPostAct>(Func<TSut, TData1, TData2, TData3, TPostAct> actFunc);
     IArrangedWithData<TSut, TData1, TData2, TData3> With(TData1 data1, TData2 data2, TData3 data3);
  }

  public interface IAssertWithDataCaseBuilder<out TSut, out TData1, out TData2, out TData3>
  {
      IPostAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3> Assert(Action<TSut, TData1, TData2, TData3> assertAction);
      IPostAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3> Assert(string assertionTestCaseName, Action<TSut, TData1, TData2, TData3> assertAction);
  }

  public interface IPostAssertWithDataCaseBuilder<out TSut, out TData1, out TData2, out TData3> : ISpecification,
        IAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3>
  {
  }

  internal sealed class DataAssertBuilder<TSut, TData1, TData2, TData3> : IPostAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3>
  {
        private readonly TestCaseAccumulator _tests = new TestCaseAccumulator();
        private readonly Func<TData1, TData2, TData3, TSut> _actFunc;
        private readonly IDataSuite<Tuple<TData1, TData2, TData3>> _dataSuite;

        public DataAssertBuilder(Func<TData1, TData2, TData3, TSut> actFunc, IDataSuite<Tuple<TData1, TData2, TData3>> dataSuite)
        {
            _actFunc = actFunc;
            _dataSuite = dataSuite;
        }

        public IPostAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3> Assert(Action<TSut, TData1, TData2, TData3> assertMethod)
        {
            InternalAssert(_dataSuite.SuiteName, assertMethod);
            return this;
        }

        public IPostAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3> Assert(string assertionTestCaseName,
            Action<TSut, TData1, TData2, TData3> assertMethod)
        {
            InternalAssert(_dataSuite.SuiteName + " " + assertionTestCaseName, assertMethod);
            return this;
        }

        private void InternalAssert(string testName, Action<TSut, TData1, TData2, TData3> assertMethod)
        {
            foreach (var data in _dataSuite.Data)
            {
                var d = data;
                string inject = NameInjection.Inject(testName, d);
                Action assertTestMethod = () =>
                {
                    TSut acted = _actFunc(d.Item1, d.Item2, d.Item3);
                    assertMethod(acted, d.Item1, d.Item2, d.Item3);
                };
                _tests.AddSingleTest(inject, assertTestMethod);
            }
        }

        public IEnumerable<ISingleRunnableTestCase> EmitAllRunnableTests()
        {
            return _tests.EmitAllRunnableTests();
        }
    }

	internal sealed class DataPreAssertBuilder<TSut, TData1, TData2, TData3> : IAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3>
    {
        private readonly Func<TData1, TData2, TData3, TSut> _actFunc;
        private readonly IDataSuite<Tuple<TData1, TData2, TData3>> _dataSuite;

        public DataPreAssertBuilder(Func<TData1, TData2, TData3, TSut> actFunc, IDataSuite<Tuple<TData1, TData2, TData3>> dataSuite)
        {
            _actFunc = actFunc;
            _dataSuite = dataSuite;
        }

        public IPostAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3> Assert(Action<TSut, TData1, TData2, TData3> assertAction)
        {
            return new DataAssertBuilder<TSut, TData1, TData2, TData3>(_actFunc, _dataSuite).Assert(assertAction);
        }

        public IPostAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3> Assert(string assertionTestCaseName,
            Action<TSut, TData1, TData2, TData3> assertAction)
        {
            return new DataAssertBuilder<TSut, TData1, TData2, TData3>(_actFunc, _dataSuite).Assert(assertionTestCaseName,
                assertAction);
        }
    }

	internal sealed class ArrangedDataBuilder<TSut, TData1, TData2, TData3> : IArrangedWithData<TSut, TData1, TData2, TData3>, IDataSuite<Tuple<TData1, TData2, TData3>>
    {
        private readonly ISuite _suite;
        private readonly Func<TSut> _arrangeFunc;
        private readonly List<Tuple<TData1, TData2, TData3>> _data = new List<Tuple<TData1, TData2, TData3>>();

        public ArrangedDataBuilder(ISuite suite, Func<TSut> arrangeFunc)
        {
            _suite = suite;
            _arrangeFunc = arrangeFunc;
        }

        public IAssertWithDataCaseBuilder<TPostAct, TData1, TData2, TData3> Act<TPostAct>(Func<TSut, TData1, TData2, TData3, TPostAct> actFunc)
        {
            return new DataPreAssertBuilder<TPostAct, TData1, TData2, TData3>(
                (data1, data2, data3) =>
                {
                    var arranged = _arrangeFunc();
                    return actFunc(arranged, data1, data2, data3);
                }, this);
        }

        public IArrangedWithData<TSut, TData1, TData2, TData3> With(TData1 data1, TData2 data2, TData3 data3)
        {
            _data.Add(Tuple.Create(data1, data2, data3));
            return this;
        }

        public string SuiteName
        {
            get { return _suite.SuiteName; }
        }

        public IEnumerable<Tuple<TData1, TData2, TData3>> Data
        {
            get { return _data; }
        }
    }
}
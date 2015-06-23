// Code generated by a template
using System;
using Mercury.AssertBuilder;

namespace Mercury {


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
        private readonly IDataSuite<TData> _dataSuite;

        public DataAssertBuilder(Func<TData1, TData2, TData3, TSut> actFunc, IDataSuite<TData> dataSuite)
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
                    TSut acted = _actFunc(d);
                    assertMethod(acted, d);
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

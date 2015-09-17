// Code generated by a template
using System;
using Mercury.AssertBuilder;
using System.Collections.Generic;

namespace Mercury.AssertBuilder
{
  internal sealed class AssertBuilder<TSut, TData1, TData2, TData3, TData4, TData5> : IPostAssertCaseBuilder<TSut, TData1, TData2, TData3, TData4, TData5>
  {
        private readonly TestCaseAccumulator _tests = new TestCaseAccumulator();
        private readonly Func<TData1, TData2, TData3, TData4, TData5, TSut> _actFunc;
		private readonly IDataSuite<Tuple<TData1, TData2, TData3, TData4, TData5>> _dataSuite;

        public AssertBuilder(Func<TData1, TData2, TData3, TData4, TData5, TSut> actFunc,
                           IDataSuite<Tuple<TData1, TData2, TData3, TData4, TData5>> dataSuite)
        {
            _actFunc = actFunc;
            _dataSuite = dataSuite;
        }

        public IPostAssertCaseBuilder<TSut, TData1, TData2, TData3, TData4, TData5> Assert(Action<TSut, TData1, TData2, TData3, TData4, TData5> assertMethod)
        {
		    InternalAssert(_dataSuite.SuiteName, assertMethod);
            return this;
        }

        public IPostAssertCaseBuilder<TSut, TData1, TData2, TData3, TData4, TData5> Assert(string assertionTestCaseName,
            Action<TSut, TData1, TData2, TData3, TData4, TData5> assertMethod)
        {
            InternalAssert(_dataSuite.SuiteName + " " + assertionTestCaseName, assertMethod);
            return this;
        }

        private void InternalAssert(string testNameTemplate, Action<TSut, TData1, TData2, TData3, TData4, TData5> assertMethod)
        {
            foreach (var data in _dataSuite.Data)
            {
                var d = data;
				var testName = testNameTemplate;
				testName = NameInjection.Inject("1", testName, d.Item1);
				testName = NameInjection.Inject("2", testName, d.Item2);
				testName = NameInjection.Inject("3", testName, d.Item3);
				testName = NameInjection.Inject("4", testName, d.Item4);
				testName = NameInjection.Inject("5", testName, d.Item5);
                testName = NameInjection.Inject(testName, d);
                Action assertTestMethod = () =>
                {
                    TSut acted = _actFunc(d.Item1, d.Item2, d.Item3, d.Item4, d.Item5);
                    assertMethod(acted, d.Item1, d.Item2, d.Item3, d.Item4, d.Item5);
                };
                _tests.AddSingleTest(testName, assertTestMethod);
            }
        }

        public IEnumerable<ISingleRunnableTestCase> EmitAllRunnableTests()
        {
            return _tests.EmitAllRunnableTests();
        }
    }
}

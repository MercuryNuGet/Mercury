// Code generated by a template
using System;
using Mercury.AssertBuilder;
using System.Collections.Generic;

namespace Mercury.AssertBuilder
{
  internal sealed class AssertBuilder<TSut, TData> : IPostAssertCaseBuilder<TSut, TData>
  {
        private readonly TestCaseAccumulator _tests = new TestCaseAccumulator();
        private readonly Func<TData, TSut> _actFunc;
		private readonly IDataSuite<TData> _dataSuite;

        public AssertBuilder(Func<TData, TSut> actFunc,
                           IDataSuite<TData> dataSuite)
        {
            _actFunc = actFunc;
            _dataSuite = dataSuite;
        }

        public IPostAssertCaseBuilder<TSut, TData> Assert(Action<TSut, TData> assertMethod)
        {
		    InternalAssert(_dataSuite.SuiteName, assertMethod);
            return this;
        }

        public IPostAssertCaseBuilder<TSut, TData> Assert(string assertionTestCaseName,
            Action<TSut, TData> assertMethod)
        {
            InternalAssert(_dataSuite.SuiteName + " " + assertionTestCaseName, assertMethod);
            return this;
        }

        private void InternalAssert(string testNameTemplate, Action<TSut, TData> assertMethod)
        {
            foreach (var data in _dataSuite.Data)
            {
                var d = data;
				var testName = testNameTemplate;
				testName = NameInjection.Inject("1", testName, d);
                testName = NameInjection.Inject(testName, d);
                Action assertTestMethod = () =>
                {
                    TSut acted = _actFunc(d);
                    assertMethod(acted, d);
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
// Code generated by a template
using System;
using Mercury.AssertBuilder;
using System.Collections.Generic;

namespace Mercury.AssertBuilder
{
  internal sealed class DataAssertBuilder<TSut> : IPostAssertWithDataCaseBuilder<TSut>
  {
        private readonly TestCaseAccumulator _tests = new TestCaseAccumulator();
        private readonly Func<TSut> _actFunc;
		private readonly ISuite _suite;

        public DataAssertBuilder(Func<TSut> actFunc,
                           ISuite suite)
        {
            _actFunc = actFunc;
            _suite = suite;
        }

        public IPostAssertWithDataCaseBuilder<TSut> Assert(Action<TSut> assertMethod)
        {
		    InternalAssert(_suite.SuiteName, assertMethod);
            return this;
        }

        public IPostAssertWithDataCaseBuilder<TSut> Assert(string assertionTestCaseName,
            Action<TSut> assertMethod)
        {
            InternalAssert(_suite.SuiteName + " " + assertionTestCaseName, assertMethod);
            return this;
        }

        private void InternalAssert(string testName, Action<TSut> assertMethod)
        {
            _tests.AddSingleTest(testName, () => assertMethod(_actFunc()));            
        }

        public IEnumerable<ISingleRunnableTestCase> EmitAllRunnableTests()
        {
            return _tests.EmitAllRunnableTests();
        }
    }
}

// Code generated by a template
using System;
using Mercury.AssertBuilder;
using System.Collections.Generic;

namespace Mercury.AssertBuilder
{
  internal sealed class AssertBuilder<TSut> : IPostAssertCaseBuilder<TSut>
  {
        private readonly TestCaseAccumulator _tests = new TestCaseAccumulator();
        private readonly Func<TSut> _actFunc;
		private readonly ISuite _suite;

        public AssertBuilder(Func<TSut> actFunc,
                           ISuite suite)
        {
            _actFunc = actFunc;
            _suite = suite;
        }

        public IPostAssertCaseBuilder<TSut> Assert(Action<TSut> assertMethod)
        {
		    InternalAssert(_suite.SuiteName, assertMethod);
            return this;
        }

        public IPostAssertCaseBuilder<TSut> Assert(string assertionTestCaseName,
            Action<TSut> assertMethod)
        {
            InternalAssert(_suite.SuiteName + " " + assertionTestCaseName, assertMethod);
            return this;
        }

        private void InternalAssert(string testNameTemplate, Action<TSut> assertMethod)
        {
            _tests.AddSingleTest(testNameTemplate, () => assertMethod(_actFunc()));            
        }

        public IEnumerable<ISingleRunnableTestCase> EmitAllRunnableTests()
        {
            return _tests.EmitAllRunnableTests();
        }
    }
}

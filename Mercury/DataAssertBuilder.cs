using System;
using System.Collections.Generic;

namespace Mercury
{
    internal sealed class DataAssertBuilder<TSut, TData, TPostAct> : IDataAssertCaseBuilder<TPostAct, TData>
    {
        private TestCaseBuilder<TSut> _testCaseBuilder;
        private Func<TSut, TData, TPostAct> _actFunc;
        private readonly List<TData> _data;

        public DataAssertBuilder(TestCaseBuilder<TSut> testCaseBuilder, Func<TSut, TData, TPostAct> actFunc, List<TData> data)
        {
            _testCaseBuilder = testCaseBuilder;
            _actFunc = actFunc;
            _data = data;
        }

        public IDataAssertCaseBuilder<TPostAct, TData> Assert(Action<TPostAct, TData> assertMethod)
        {
            InternalAssert(_testCaseBuilder.TestSuiteName, assertMethod);
            return this;
        }

        public IDataAssertCaseBuilder<TPostAct, TData> Assert(string assertionTestCaseName, Action<TPostAct, TData> assertMethod)
        {
            InternalAssert(_testCaseBuilder.TestSuiteName + " " + assertionTestCaseName, assertMethod);
            return this;
        }

        private void InternalAssert(string testName, Action<TPostAct, TData> assertMethod)
        {
            foreach (var data in _data)
            {
                var d = data;
                string inject = NameInjection.Inject(testName, d);
                Action<TSut> assertTestMethod = sut =>
                {
                    TPostAct acted = _actFunc(sut, d);
                    assertMethod(acted, d);
                };
                _testCaseBuilder.InternalAssert(inject, assertTestMethod);
            }
        }

        public IEnumerable<ISingleRunnableTestCase> EmitAllRunnableTests()
        {
            return _testCaseBuilder.EmitAllRunnableTests();
        }
    }
}

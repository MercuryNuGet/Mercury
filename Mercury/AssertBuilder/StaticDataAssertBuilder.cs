using System;
using System.Collections.Generic;

namespace Mercury.AssertBuilder
{
    internal sealed class StaticDataAssertBuilder<TSut, TData> : IAssertWithDataCaseBuilder<TSut, TData>
    {
        private readonly TestCaseAccumulator _tests = new TestCaseAccumulator();
        private readonly Func<TData, TSut> _actFunc;
        private readonly IDataSuite<TData> _dataSuite;

        public StaticDataAssertBuilder(Func<TData, TSut> actFunc, IDataSuite<TData> dataSuite)
        {
            _actFunc = actFunc;
            _dataSuite = dataSuite;
        }

        public IAssertWithDataCaseBuilder<TSut, TData> Assert(Action<TSut, TData> assertMethod)
        {
            InternalAssert(_dataSuite.SuiteName, assertMethod);
            return this;
        }

        public IAssertWithDataCaseBuilder<TSut, TData> Assert(string assertionTestCaseName,
            Action<TSut, TData> assertMethod)
        {
            InternalAssert(_dataSuite.SuiteName + " " + assertionTestCaseName, assertMethod);
            return this;
        }

        private void InternalAssert(string testName, Action<TSut, TData> assertMethod)
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
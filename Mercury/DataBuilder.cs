using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury
{
    internal sealed class DataAssertBuilder<TSut, TData, TPostAct> : IParamertizedDynamicAssertCaseBuilder<TPostAct, TData>
    {
        private TestCaseBuilder<TSut> _testCaseBuilder;
        private Func<TSut, TData, TPostAct> _actFunc;
        private readonly List<TData> _data;

        public DataAssertBuilder(TestCaseBuilder<TSut> _testCaseBuilder, Func<TSut, TData, TPostAct> actFunc, List<TData> data)
        {
            _testCaseBuilder = _testCaseBuilder;
            _actFunc = actFunc;
            _data = data;
        }

        public IParamertizedDynamicAssertCaseBuilder<TPostAct, TData> Assert(Action<TPostAct, TData> dynamicAssertMethod)
        {
            InternalAssert(_testCaseBuilder.TestSuiteName, dynamicAssertMethod);
            return this;
        }

        public IParamertizedDynamicAssertCaseBuilder<TPostAct, TData> Assert(string assertionTestCaseName, Action<TPostAct, TData> dynamicAssertMethod)
        {
            InternalAssert(_testCaseBuilder.TestSuiteName + " " + assertionTestCaseName, dynamicAssertMethod);
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

    internal class DataBuilder<TSut, TData> : IParamertizedDynamicArrangedTest<TSut, TData>
    {
        private readonly List<TData> _data = new List<TData>();
        private readonly TestCaseBuilder<TSut> _testCaseBuilder;
        private readonly Func<TSut> _arrangeFunction;

        public DataBuilder(TestCaseBuilder<TSut> testCaseBuilder)
        {
            _testCaseBuilder = testCaseBuilder;
        }

        public IParamertizedDynamicArrangedTest<TSut, TData> With(TData data)
        {
            _data.Add(data);
            return this;
        }

        public IParamertizedDynamicAssertCaseBuilder<TResult, TData> Act<TResult>(Func<TSut, TData, TResult> actFunc)
        {
            return new DataAssertBuilder<TSut, TData, TResult>(_testCaseBuilder, actFunc, _data);
        }

        public IParamertizedDynamicAssertCaseBuilder<TSut, TData> Assert(Action<TSut, TData> assertMethod)
        {
            return Act((sut, data) => sut).Assert(assertMethod);
        }

        public IParamertizedDynamicAssertCaseBuilder<TSut, TData> Assert(string assertionTestCaseName, Action<TSut, TData> assertMethod)
        {
            return Act((sut, data) => sut).Assert(assertionTestCaseName, assertMethod);
        }

        public IEnumerable<ISingleRunnableTestCase> EmitAllRunnableTests()
        {
            return _testCaseBuilder.EmitAllRunnableTests();
        }
    }
}

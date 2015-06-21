using System;
using System.Collections.Generic;

namespace Mercury
{
    internal sealed class TestCaseBuilder<TSut> : IArranged<TSut>
    {
        public string TestSuiteName { get; private set; }
        public Func<TSut> ArrangeMethod { get; set; }
        public Func<dynamic, TSut> DynamicArrangeMethod { get; set; }
        private readonly List<ISingleRunnableTestCase> _builtTests = new List<ISingleRunnableTestCase>();
        private readonly List<dynamic> _data = new List<dynamic>();

        public TestCaseBuilder(string testSuiteName, Func<TSut> arrangeMethod,
            IEnumerable<ISingleRunnableTestCase> builtTests)
        {
            TestSuiteName = testSuiteName;
            ArrangeMethod = arrangeMethod;
            if (builtTests != null)
                _builtTests.AddRange(builtTests);
        }

        public TestCaseBuilder(string testSuiteName, Func<dynamic, TSut> arrangeMethod,
            IEnumerable<ISingleRunnableTestCase> builtTests, IEnumerable<dynamic> data)
        {
            TestSuiteName = testSuiteName;
            DynamicArrangeMethod = arrangeMethod;
            if (builtTests != null)
                _builtTests.AddRange(builtTests);
            if (data != null)
                _data.AddRange(data);
        }

        public IAssertCaseBuilder<TResult> Act<TResult>(Func<TSut, TResult> actFunc)
        {
            return new TestCaseBuilder<TResult>(TestSuiteName, () => actFunc(ArrangeMethod()), _builtTests);
        }

        public IAssertCaseBuilder<TSut> Assert(Action<TSut> assertTestMethod)
        {
            InternalAssert(TestSuiteName, assertTestMethod);
            return this;
        }

        public IAssertCaseBuilder<TSut> Assert(string assertionTestCaseName, Action<TSut> assertTestMethod)
        {
            InternalAssert(TestSuiteName + " " + assertionTestCaseName, assertTestMethod);
            return this;
        }

        public IParamertizedDynamicArrangedTest<TSut, TData> With<TData>(TData data)
        {
            return new DataBuilder<TSut, TData>(this).With(data);
        }

        internal void InternalAssert(string name, Action<TSut> assertTestMethod)
        {
            InternalAssert(name, assertTestMethod, ArrangeMethod);
        }

        private void InternalAssert(string name, Action<TSut> assertTestMethod, Func<TSut> arrangeMethod)
        {
            var concreteTest = new SingleRunnableTestCase(name, () =>
            {
                var arrange = arrangeMethod();
                assertTestMethod(arrange);
            });
            _builtTests.Add(concreteTest);
        }

        public IEnumerable<ISingleRunnableTestCase> EmitAllRunnableTests()
        {
            return _builtTests;
        }
    }
}
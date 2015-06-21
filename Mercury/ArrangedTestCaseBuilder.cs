using System;
using System.Collections.Generic;

namespace Mercury
{
    internal sealed class TestCaseBuilder<TSut> : IArranged<TSut>, IParamertizedDynamicArrangedTest<TSut>
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

        public IParamertizedDynamicAssertCaseBuilder<TResult> Act<TResult>(Func<TSut, dynamic, TResult> actFunc)
        {
            return new TestCaseBuilder<TResult>(TestSuiteName, d => actFunc(ArrangeMethod(), d), _builtTests, _data);
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

        public IParamertizedDynamicArrangedTest<TSut> With(dynamic data)
        {
            _data.Add(data);
            return this;
        }

        public IParamertizedDynamicArrangedTest<TSut> Assert(Action<TSut, dynamic> dynamicAssertMethod)
        {
            InternalDynamicAssert(TestSuiteName, dynamicAssertMethod);
            return this;
        }

        public IParamertizedDynamicArrangedTest<TSut> Assert(string assertionTestCaseName,
            Action<TSut, dynamic> dynamicAssertMethod)
        {
            InternalDynamicAssert(TestSuiteName + " " + assertionTestCaseName, dynamicAssertMethod);
            return this;
        }

        private void InternalDynamicAssert(string testName, Action<TSut, dynamic> dynamicAssertMethod)
        {
            if (DynamicArrangeMethod != null)
            {
                foreach (var data in _data)
                {
                    var d = data;
                    string inject = NameInjection.Inject(testName, d);
                    Func<TSut> dynamicArrangeMethod = () => DynamicArrangeMethod(d);
                    Action<TSut> assertTestMethod = sut => dynamicAssertMethod(sut, d);
                    InternalAssert(inject, assertTestMethod, dynamicArrangeMethod);
                }
            }
            else
            {
                foreach (var data in _data)
                {
                    var d = data;
                    string inject = NameInjection.Inject(testName, d);
                    InternalAssert(inject, sut => dynamicAssertMethod(sut, d), ArrangeMethod);
                }
            }
        }

        private void InternalAssert(string name, Action<TSut> assertTestMethod)
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
using System;
using System.Collections.Generic;

namespace Mercury
{
    internal interface ITestCaseBuilder<TSut> : ISpecification
    {
        string TestSuiteName { get; }
        void AddSingleTest(string testCaseName, Action<TSut> postArrangeTestMethod);
    }

    internal sealed class TestCaseBuilder<TSut> : IArranged<TSut>, ITestCaseBuilder<TSut>
    {
        public string TestSuiteName { get; private set; }
        public Func<TSut> ArrangeMethod { get; set; }
        private readonly List<ISingleRunnableTestCase> _builtTests = new List<ISingleRunnableTestCase>();

        public TestCaseBuilder(string testSuiteName, Func<TSut> arrangeMethod,
            IEnumerable<ISingleRunnableTestCase> builtTests)
        {
            TestSuiteName = testSuiteName;
            ArrangeMethod = arrangeMethod;
            if (builtTests != null)
                _builtTests.AddRange(builtTests);
        }

        public IAssertCaseBuilder<TResult> Act<TResult>(Func<TSut, TResult> actFunc)
        {
            return new TestCaseBuilder<TResult>(TestSuiteName, () => actFunc(ArrangeMethod()), _builtTests);
        }

        public IAssertCaseBuilder<TSut> Assert(Action<TSut> assertTestMethod)
        {
            AddSingleTest(TestSuiteName, assertTestMethod);
            return this;
        }

        public IAssertCaseBuilder<TSut> Assert(string assertionTestCaseName, Action<TSut> assertTestMethod)
        {
            AddSingleTest(TestSuiteName + " " + assertionTestCaseName, assertTestMethod);
            return this;
        }

        public IDataArrangedTest<TSut, TData> With<TData>(TData data)
        {
            return new DataBuilder<TSut, TData>(this).With(data);
        }

        public void AddSingleTest(string testCaseName, Action<TSut> postArrangeTestMethod)
        {
            var concreteTest = new SingleRunnableTestCase(testCaseName, () =>
            {
                var arrange = ArrangeMethod();
                postArrangeTestMethod(arrange);
            });
            _builtTests.Add(concreteTest);
        }

        public IEnumerable<ISingleRunnableTestCase> EmitAllRunnableTests()
        {
            return _builtTests;
        }
    }
}
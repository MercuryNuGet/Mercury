using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Mercury
{
    public static class Extensions
    {
        public static IPostAssertCaseBuilder<T> AssertEquals<T>(this IAssertCaseBuilder<T> builder,
            T expected)
        {
            return builder.Assert(string.Format("is equal to {0}", expected),
                result => Assert.AreEqual(expected, result));
        }

        /// <summary>
        ///     Use ActOn to keep the test context. Must use for calling voids.
        /// </summary>
        /// <typeparam name="T">The test context type</typeparam>
        /// <param name="arrangedTest">The arranged test context</param>
        /// <param name="action">Action to perform on test context</param>
        /// <returns>The original test context</returns>
        public static IAssertCaseBuilder<T> ActOn<T>(this IArranged<T> arrangedTest, Action<T> action)
        {
            return arrangedTest.Act(sut =>
            {
                action(sut);
                return sut;
            });
        }

        /// <summary>
        ///     Use ActOn to keep the test context. Must use for calling voids.
        /// </summary>
        /// <typeparam name="T">The test context type</typeparam>
        /// <typeparam name="TData">The data type</typeparam>
        /// <param name="arrangedTest">The arranged test context</param>
        /// <param name="action">Action to perform on test context</param>
        /// <returns>The original test context</returns>
        public static IAssertWithDataCaseBuilder<T, TData> ActOn<T, TData>(
            this IArrangedWithData<T, TData> arrangedTest,
            Action<T, TData> action)
        {
            return arrangedTest.Act((sut, d) =>
            {
                action(sut, d);
                return sut;
            });
        }

        public static ISpecification Branch<TSut>(this IPostAssertCaseBuilder<TSut> specification,
            string branchName,
            Func<IArranged<TSut>, ISpecification[]> map)
        {
            var a = new A<TSut>();
            var tests = specification.EmitAllRunnableTests();
            foreach (var test in tests)
            {
                var test1 = (ISingleRunnableTestCase<TSut>) test;
                a.Add(test1);
                var spec = (test.Name + " " + branchName).Arrange(() => test1.TestMethodWithResult());
                var specs = map(spec);
                a.Add(specs);
            }
            return a;
        }

        private sealed class A<TResult> : ISpecification
        {
            private readonly List<ISingleRunnableTestCase<TResult>> _builtTests =
                new List<ISingleRunnableTestCase<TResult>>();

            public IEnumerable<ISingleRunnableTestCase> EmitAllRunnableTests()
            {
                return _builtTests.ToList();
            }

            public void Add(ISingleRunnableTestCase<TResult> test1)
            {
                _builtTests.Add(test1);
            }

            public void Add(ISpecification[] specs)
            {
                foreach (var specification in specs)
                {
                    if (specification is ISingleRunnableTestCase<TResult>)
                        Add(specification as ISingleRunnableTestCase<TResult>);
                    else
                    {
                        var tests = specification.EmitAllRunnableTests();
                        foreach (var singleRunnableTestCase in tests)
                        {
                            if (singleRunnableTestCase is ISingleRunnableTestCase<TResult>)
                                Add(singleRunnableTestCase as ISingleRunnableTestCase<TResult>);
                            else
                            {
                                ISingleRunnableTestCase @case = singleRunnableTestCase;
                                Add(new SingleRunnableTestCase<TResult>(
                                    singleRunnableTestCase.Name,
                                    () =>
                                    {
                                        @case.TestMethod();
                                        return default (TResult);
                                    }));
                            }
                        }
                    }
                }
            }
        }
    }
}
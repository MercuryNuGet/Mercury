using System;

namespace Mercury
{
    public static class Extensions
    {
        public static IStaticAssertCaseBuilder<T> AssertEquals<T>(this IStaticPreAssertCaseBuilder<T> builder,
            T expected)
        {
            return builder.Assert(string.Format("is equal to {0}", expected),
                result => NUnit.Framework.Assert.AreEqual(expected, result));
        }

        /// <summary>
        ///     Use ActOn to keep the test context. Must use for calling voids.
        /// </summary>
        /// <typeparam name="T">The test context type</typeparam>
        /// <param name="arrangedTest">The arranged test context</param>
        /// <param name="action">Action to perform on test context</param>
        /// <returns>The original test context</returns>
        public static IStaticPreAssertCaseBuilder<T> ActOn<T>(this IArranged<T> arrangedTest, Action<T> action)
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
        public static IPreAssertWithDataCaseBuilder<T, TData> ActOn<T, TData>(
            this ISutArrangedWithData<T, TData> arrangedTest,
            Action<T, TData> action)
        {
            return arrangedTest.Act((sut, d) =>
            {
                action(sut, d);
                return sut;
            });
        }

        public static IAssertWithDataCaseBuilder<TSut, TData> Assert<TSut, TData>(
            this ISutArrangedWithData<TSut, TData> arranged, Action<TSut, TData> assertAction)
        {
            return arranged.Act((sut, data) => sut).Assert(assertAction);
        }
    }
}
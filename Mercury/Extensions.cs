using System;
using NUnit.Framework;

namespace Mercury
{
    public static class Extensions
    {
        public static IAssertCaseBuilder<T> AssertEquals<T>(this IAssertCaseBuilder<T> builder, T expected)
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
        /// <param name="arrangedTest">The arranged test context</param>
        /// <param name="action">Action to perform on test context</param>
        /// <returns>The original test context</returns>
        public static IDataAssertCaseBuilder<T, TData> ActOn<T, TData>(this IDataArrangedTest<T, TData> arrangedTest,
            Action<T, TData> action)
        {
            return arrangedTest.Act((sut, d) =>
            {
                action(sut, d);
                return sut;
            });
        }
    }
}
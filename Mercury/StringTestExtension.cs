﻿using System;
using Mercury.Arrange;
using Mercury.StaticArrange;

namespace Mercury
{
    public static class StringTestExtension
    {
        /// <summary>
        ///     Use to arrange a test context from the given arrange method
        /// </summary>
        /// <typeparam name="T">The type of the test context</typeparam>
        /// <param name="testName">The test name</param>
        /// <param name="arrangeMethod">The method that will be called to create the test context</param>
        /// <returns>An arranged test context</returns>
        public static IArranged<T> Arrange<T>(this string testName, Func<T> arrangeMethod)
        {
            return new ArrangedTestBuilder<T>(testName, arrangeMethod);
        }

        public static ISpecification Assert(this string testName, Action test)
        {
            return new SingleRunnableTestCase(testName, test);
        }

        /// <summary>
        ///     Use to arrange a test context from a class that has a public parameterless constructor
        /// </summary>
        /// <typeparam name="T">The type of the test context</typeparam>
        /// <param name="testName">The test name</param>
        /// <returns>An arranged test context</returns>
        public static IArranged<T> Arrange<T>(this string testName) where T : new()
        {
            return testName.Arrange(() => new T());
        }

        /// <summary>
        ///     Use if you do not require a test context. Creates a null test context of type object
        /// </summary>
        /// <param name="testName">The test name</param>
        /// <returns>Arrange test with null context</returns>
        public static IArranged<object> ArrangeNull(this string testName)
        {
            return testName.Arrange<object>(() => null);
        }

        /// <summary>
        ///     Use if you do not require a test context.
        /// </summary>
        /// <param name="testName">The test name</param>
        /// <returns>Arranged test with no context</returns>
        public static IStaticArranged Arrange(this string testName)
        {
            return new StaticArrangedTestBuilder(testName);
        }

        /// <summary>
        ///     Use to arrange a test context that has a parameterless constuctor but you want to run some further setup.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="testName"></param>
        /// <param name="arrangeAction"></param>
        /// <returns></returns>
        public static IArranged<T> Arrange<T>(this string testName, Action<T> arrangeAction) where T : new()
        {
            return testName.Arrange(() =>
            {
                var context = new T();
                arrangeAction(context);
                return context;
            });
        }
    }
}
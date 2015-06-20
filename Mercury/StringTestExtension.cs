using System;

namespace Mercury
{
    public static class StringTestExtension
    {
        /// <summary>
        /// Use to arrange a test context from the given arrange method
        /// </summary>
        /// <typeparam name="T">The type of the test context</typeparam>
        /// <param name="str">The test name</param>
        /// <param name="arrangeMethod">The method that will be called to create the test context</param>
        /// <returns>An arranged test context</returns>
        public static IArranged<T> Arrange<T>(this string str, Func<T> arrangeMethod)
        {
            return new Arranged<T>(str, arrangeMethod, null);
        }

        public static ISpecification Assert(this string str, Action test)
        {
            return new SingleRunnableQuickSilverCase(str, test);
        }

        /// <summary>
        /// Use to arrange a test context from a class that has a public parameterless constructor
        /// </summary>
        /// <typeparam name="T">The type of the test context</typeparam>
        /// <param name="str">The test name</param>
        /// <returns>An arranged test context</returns>
        public static IArranged<T> Arrange<T>(this string str) where T : new()
        {
            return str.Arrange(() => new T());
        }
        
        /// <summary>
        /// Use if you do not require a test context. Creates a null test context of type object
        /// </summary>
        /// <param name="str">The test name</param>
        /// <returns>Arrange test with null context</returns>
        public static IArranged<object> Arrange(this string str)
        {
            return str.Arrange<object>(() => null);
        }
    }
}
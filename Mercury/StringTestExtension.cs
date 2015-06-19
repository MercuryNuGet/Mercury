using System;

namespace Mercury
{
    public static class StringTestExtension
    {
        public static IArranged<T> Arrange<T>(this string str, Func<T> arrangeMethod)
        {
            return new Arranged<T>(str, arrangeMethod, null);
        }

        public static ISpecification Assert(this string str, Action test)
        {
            return new SingleRunnableQuickSilverCase(str, test);
        }

        public static IArranged<T> Arrange<T>(this string str) where T : new()
        {
            return str.Arrange(() => new T());
        }
    }
}
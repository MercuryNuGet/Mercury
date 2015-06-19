using System;

namespace Mercury
{
    public static class StringTestExtension
    {
        public static IArrangedQuickSilver<T> Arrange<T>(this string str, Func<T> arrangeMethod)
        {
            return new ArrangedQuickSilver<T>(str, arrangeMethod, null);
        }

        public static ISpecification Assert(this string str, Action test)
        {
            return new SingleRunnableQuickSilverCase(str, test);
        }

        public static IArrangedQuickSilver<T> Arrange<T>(this string str) where T : new()
        {
            return str.Arrange(() => new T());
        }
    }
}
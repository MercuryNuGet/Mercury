using NUnit.Framework;

namespace Mercury
{
    public static class Extensions
    {
        public static IAssertCaseBuilder<T> AssertEquals<T>(this IAssertCaseBuilder<T> builder, T expected)
        {
            return builder.Assert(string.Format("is equal to {0}", expected), result => Assert.AreEqual(expected, result));
        }

        public static IArranged<object> Arrange(this string str)
        {
            return str.Arrange<object>();
        }
    }
}
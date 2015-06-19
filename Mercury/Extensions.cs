using NUnit.Framework;

namespace Mercury
{
    public static class Extensions
    {
        public static IAssertQuickSilverCaseBuilder<T> AssertEquals<T>(this IAssertQuickSilverCaseBuilder<T> builder, T expected)
        {
            return builder.Assert(string.Format("is equal to {0}", expected), result => Assert.AreEqual(expected, result));
        }

        public static IArrangedQuickSilver<object> Arrange(this string str)
        {
            return str.Arrange<object>();
        }
    }
}
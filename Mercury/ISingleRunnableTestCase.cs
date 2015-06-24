using System;

namespace Mercury
{
    public interface ISingleRunnableTestCase
    {
        string Name { get; }
        Action TestMethod { get; }
        void Run();
    }

    public interface ISingleRunnableTestCase<out TResult> : ISingleRunnableTestCase
    {
        Func<TResult> TestMethodWithResult { get; }
    }
}
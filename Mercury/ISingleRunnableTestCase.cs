using System;

namespace Mercury
{
    public interface ISingleRunnableTestCase
    {
        string Name { get; }
        Action TestMethod { get; }
        void Run();
    }
}
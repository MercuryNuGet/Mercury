using Mercury;

namespace MercuryTests
{
    public static class TestUtil
    {
        public static void RunAll(ISpecification spec)
        {
            foreach (var test in spec.EmitAllRunnableTests())
                test.Run();
        }
    }
}
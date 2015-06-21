using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Mercury
{
    [TestFixture]
    public abstract class Specification : S
    {
    }

    public abstract class S
    {
        protected abstract ISpecification[] TestCases();

        protected ISingleRunnableTestCase[] CreateCases()
        {
            var testCases = TestCases().SelectMany(t => t.EmitAllRunnableTests()).ToArray();
            return TestCaseNameClashRenamer.RenameClashingTests(testCases);
        }

        [Test, TestCaseSource("CreateCases")]
        public void T(ISingleRunnableTestCase testCase)
        {
            testCase.Run();
        }
    }
}
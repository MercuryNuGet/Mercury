using System.Collections;
using System.Linq;
using NUnit.Framework;

namespace Mercury
{
    [TestFixture]
    public abstract class Specification : m
    {
    }

    public abstract class m
    {
        protected abstract ISpecification[] TestCases();

        protected IEnumerable CreateCases()
        {
            ISingleRunnableTestCase[] testCases = TestCases().SelectMany(t => t.EmitAllRunnableTests()).ToArray();
            testCases = TestCaseNameClashRenamer.RenameClashingTests(testCases);
            return testCases.Select(t => new TestCaseData(t).SetName(t.Name));
        }

        [Test, TestCaseSource("CreateCases")]
        public void TestMercuryTestCases(ISingleRunnableTestCase testCase)
        {
            testCase.Run();
        }
    }
}
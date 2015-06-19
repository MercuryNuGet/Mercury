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
            var testCases = new List<ISingleRunnableTestCase>(TestCases().SelectMany(t => t.GetAll()));

            return RenameTestsWithClashingNames(testCases);
        }

        private static ISingleRunnableTestCase[] RenameTestsWithClashingNames(
            IEnumerable<ISingleRunnableTestCase> testCases)
        {
            var renamedTestCases = new List<ISingleRunnableTestCase>();

            var groupedTests = testCases.GroupBy(c => c.Name);

            foreach (var groupedTest in groupedTests)
            {
                var count = groupedTest.Count();
                if (count == 1)
                {
                    renamedTestCases.Add(groupedTest.Single());
                    continue;
                }
                var idx = 1;
                foreach (var element in groupedTest)
                {
                    renamedTestCases.Add(new SingleRunnableQuickSilverCase(string.Format("{0} : {1}", element.Name, idx),
                        element.TestMethod));
                    idx++;
                }
            }

            return renamedTestCases.ToArray();
        }

        [Test, TestCaseSource("CreateCases")]
        public void T(ISingleRunnableTestCase testCase)
        {
            testCase.Run();
        }
    }

    public interface ISpecification
    {
        IEnumerable<ISingleRunnableTestCase> GetAll();
    }
}
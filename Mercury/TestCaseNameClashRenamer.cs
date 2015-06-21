﻿using System.Linq;

namespace Mercury
{
    internal static class TestCaseNameClashRenamer
    {
        internal static ISingleRunnableTestCase[] RenameClashingTests(ISingleRunnableTestCase[] testCases)
        {
            var renamedTestCases = testCases.ToArray();

            var groupedTests = testCases
                .Select((value, index) => new
                {
                    Value = value,
                    Index = index,
                    value.Name
                })
                .GroupBy(c => c.Name);

            foreach (var groupedTest in groupedTests.Where(g => g.Count() > 1))
            {
                var repeatNumber = 1;
                foreach (var element in groupedTest)
                {
                    var test = element.Value;
                    var newTestName = string.Format("{0} : {1}", test.Name, repeatNumber);
                    renamedTestCases[element.Index] = new SingleRunnableTestCase(newTestName, element.Value.TestMethod);
                    repeatNumber++;
                }
            }

            return renamedTestCases;
        }
    }
}

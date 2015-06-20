using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Mercury;

namespace MercuryTests
{
    [TestFixture]
    public sealed class SingleRunnableTestCasesTests
    {
        [TestCase("Name")]
        [TestCase("Test name")]
        public void Name_is_set(string name)
        {
            ISingleRunnableTestCase testCase = new SingleRunnableTestCase(name, () => { });
            Assert.AreSame(name, testCase.Name);
            Assert.AreSame(name, testCase.ToString());
        }

        [Test]
        public void Name_is_set_alternative_value()
        {
            ISingleRunnableTestCase testCase = new SingleRunnableTestCase("Test name", () => { });
            Assert.AreEqual("Test name", testCase.Name);
        }

        [Test]
        public void Does_not_invoke_before_run()
        {
            var invoke = 0;
            ISingleRunnableTestCase testCase = new SingleRunnableTestCase("Name", () => invoke++);
            Assert.AreEqual(0, invoke);
        }

        [Test]
        public void Does_invoke_on_run()
        {
            var invoke = 0;
            ISingleRunnableTestCase testCase = new SingleRunnableTestCase("Name", () => invoke++);
            testCase.Run();
            Assert.AreEqual(1, invoke);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void Propergates_exceptions()
        {
            ISingleRunnableTestCase testCase = new SingleRunnableTestCase("Name", () => { throw new Exception(); });
            testCase.Run();
        }

        [Test]
        public void Emits_self()
        {
            ISpecification testCase = new SingleRunnableTestCase("Name", () => { });
            var emitedTestCase = testCase.EmitAllRunnableTests().Single();
            Assert.AreSame(testCase, emitedTestCase);
        }
    }
}

using System;
using System.Linq;
using Mercury;
using NUnit.Framework;

namespace MercuryTests
{
    [TestFixture]
    public sealed class TestCaseNameClashRenamerTests
    {
        private ISingleRunnableTestCase NewSpecWithName(string testName)
        {
            return NewSpecWithName(testName, () => { });
        }

        private ISingleRunnableTestCase NewSpecWithName(string testName, Action action)
        {
            return new SingleRunnableTestCase(testName, action);
        }

        private static ISingleRunnableTestCase[] DoRename(ISingleRunnableTestCase[] specs)
        {
            var renamedSpecs = TestCaseNameClashRenamer.RenameClashingTests(specs);
            AssertSameLengthAndNotSameInstance(specs, renamedSpecs);
            return renamedSpecs;
        }

        private static void AssertSameLengthAndNotSameInstance(ISingleRunnableTestCase[] specs, ISingleRunnableTestCase[] renamedSpecs)
        {
            Assert.AreEqual(specs.Length, renamedSpecs.Length);
            Assert.AreNotSame(specs, renamedSpecs);
        }

        [Test]
        public void Can_rename_empty_list()
        {
            ISingleRunnableTestCase[] specs = new ISingleRunnableTestCase[0];
            DoRename(specs);
        }

        [Test]
        public void Can_rename_a_single_item_list()
        {
            var specs = new ISingleRunnableTestCase[]
            {
                NewSpecWithName("TestA")
            };
            var renamedSpecs = DoRename(specs);
            Assert.AreSame(specs[0], renamedSpecs.Single());
        }

        [Test]
        public void Can_rename_two_items_that_do_not_clash()
        {
            var specs = new ISingleRunnableTestCase[]
            {
                NewSpecWithName("TestA"),
                NewSpecWithName("TestB"),
            };
            var renamedSpecs = DoRename(specs);
            Assert.AreEqual(specs, renamedSpecs);
        }

        [Test]
        public void Can_rename_two_items_that_do_clash()
        {
            var specs = new ISingleRunnableTestCase[]
            {
                NewSpecWithName("TestA"),
                NewSpecWithName("TestA"),
            };
            var renamedSpecs = DoRename(specs);
            Assert.AreEqual("TestA : 1", renamedSpecs[0].Name);
            Assert.AreEqual("TestA : 2", renamedSpecs[1].Name);
        }

        [Test]
        public void Methods_are_set_after_rename()
        {
            Action m1 = () => { };
            Action m2 = () => { };
            Assert.AreNotSame(m1, m2);
            var specs = new ISingleRunnableTestCase[]
            {
                NewSpecWithName("TestA", m1),
                NewSpecWithName("TestA", m2),
            };
            var renamedSpecs = DoRename(specs);
            Assert.AreEqual(m1, renamedSpecs[0].TestMethod);
            Assert.AreEqual(m2, renamedSpecs[1].TestMethod);
        }

        [Test]
        public void Can_rename_two_items_that_do_clash_but_are_apart_and_keep_order()
        {
            var specs = new ISingleRunnableTestCase[]
            {
                NewSpecWithName("TestA"),
                NewSpecWithName("TestB"),
                NewSpecWithName("TestA"),
            };
            var renamedSpecs = DoRename(specs);
            Assert.AreEqual("TestA : 1", renamedSpecs[0].Name);
            Assert.AreSame(specs[1], renamedSpecs[1]);
            Assert.AreEqual("TestA : 2", renamedSpecs[2].Name);
        }

        [Test]
        public void Can_rename_two_separate_clashes_and_keep_order()
        {
            var specs = new ISingleRunnableTestCase[]
            {
                NewSpecWithName("TestA"),
                NewSpecWithName("TestB"),
                NewSpecWithName("TestA"),
                NewSpecWithName("TestB"),
            };
            var renamedSpecs = DoRename(specs);
            Assert.AreEqual(specs.Length, renamedSpecs.Length);
            Assert.AreEqual("TestA : 1", renamedSpecs[0].Name);
            Assert.AreEqual("TestB : 1", renamedSpecs[1].Name);
            Assert.AreEqual("TestA : 2", renamedSpecs[2].Name);
            Assert.AreEqual("TestB : 2", renamedSpecs[3].Name);
        }
    }
}

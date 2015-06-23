using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mercury;
using NUnit.Framework;

namespace MercuryExamples
{
    internal class ReadmeExamples : Specification
    {
        protected override ISpecification[] TestCases()
        {
            return new[]
            {
                "Simple assert".Assert(() => Assert.AreEqual(2, 1 + 1)),
                "New List"
                    .Arrange(() => new List<int>())
                    .Assert("is empty", list => Assert.AreEqual(0, list.Count)),
                "No context needed because acting on static method"
                    .Arrange()
                    .Act(() => Path.Combine("a", "b"))
                    .Assert(path => Assert.AreEqual(@"a\b", path)),
                "New List; linq says there is not any"
                    .Arrange<List<int>>()
                    .Act(list => list.Any())
                    .Assert(any => Assert.IsFalse(any)),
                "New List; linq says there is not any"
                    .Arrange<List<int>>()
                    .Act(list => list.Any())
                    .Assert(Assert.IsFalse),
                "Act version"
                    .Arrange(() => new List<int>(new[] {1, 2, 3}))
                    .Act(list =>
                    {
                        list.Clear();
                        return list;
                    })
                    .Assert(list => Assert.AreEqual(0, list.Count)),
                "ActOn version"
                    .Arrange(() => new List<int>(new[] {1, 2, 3}))
                    .ActOn(list => list.Clear())
                    .Assert(list => Assert.AreEqual(0, list.Count)),
                "When I add an item to list"
                    .Arrange<List<int>>()
                    .With(new {a = 1})
                    .ActOn((list, data) => list.Add(data.a))
                    .Assert("it is exactly one long",
                        (list, data) => Assert.AreEqual(1, list.Count)),
                "Test-Context less using with"
                    .Arrange()
                    .With(new {a = "a", b = "b", expect = @"a\b"})
                    .With(new {a = "c", b = "d", expect = @"c\d"})
                    .Act(data => Path.Combine(data.a, data.b))
                    .Assert((actual, data) => Assert.AreEqual(data.expect, actual)),
                "When I add #a item to list"
                    .Arrange<List<int>>()
                    .With(new {a = 1})
                    .With(new {a = 2})
                    .ActOn((list, data) => list.Add(data.a))
                    .Assert("it is exactly one long",
                        (list, data) => Assert.AreEqual(1, list.Count)),
                "When I add #a to list"
                    .Arrange<List<int>>()
                    .With(new {a = 1})
                    .With(new {a = 2})
                    .ActOn((list, data) => list.Add(data.a))
                    .Assert("it is exactly one long",
                        (list, data) => Assert.AreEqual(1, list.Count))
                    .Assert("it contains #a",
                        (list, data) => Assert.AreEqual(data.a, list[0])),
                "When I add #expectedLength items to list"
                    .Arrange<List<int>>()
                    .With(new {a = new[] {1, 2, 3}, expectedLength = 3, expectedSum = 6})
                    .With(new {a = new[] {4, 6, 7, 9}, expectedLength = 4, expectedSum = 26})
                    .ActOn((list, data) => list.AddRange(data.a))
                    .Assert("it is exactly #expectedLength long",
                        (list, data) => Assert.AreEqual(data.expectedLength, list.Count))
                    .Assert("the sum is #expectedSum",
                        (list, data) => Assert.AreEqual(data.expectedSum, list.Sum()))
            };
        }
    }
}
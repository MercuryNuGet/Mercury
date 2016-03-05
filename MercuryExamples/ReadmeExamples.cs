using Mercury;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MercuryExamples
{
    internal class ReadmeExamples : MercurySuite
    {
        protected override void Specifications()
        {
            Specs +=
                 "Simple assert".Assert(() => Assert.AreEqual(2, 1 + 1));

            Specs +=
                "New List"
                    .Arrange(() => new List<int>())
                    .Assert("is empty", list => Assert.AreEqual(0, list.Count));

            Specs +=
                "No context needed because acting on static method"
                    .Arrange()
                    .Act(() => string.Join(",", "a", "b"))
                    .Assert(path => Assert.AreEqual("a,b", path));

            Specs +=
                "New List; linq says there is not any"
                    .Arrange<List<int>>()
                    .Act(list => list.Any())
                    .Assert(any => Assert.IsFalse(any));

            Specs +=
                "New List; linq says there is not any"
                    .Arrange<List<int>>()
                    .Act(list => list.Any())
                    .Assert(Assert.IsFalse);

            Specs +=
                "Act version"
                    .Arrange(() => new List<int>(new[] { 1, 2, 3 }))
                    .Act(list =>
                    {
                        list.Clear();
                        return list;
                    })
                    .Assert(list => Assert.AreEqual(0, list.Count));

            Specs +=
                "ActOn version"
                    .Arrange(() => new List<int>(new[] { 1, 2, 3 }))
                    .ActOn(list => list.Clear())
                    .Assert(list => Assert.AreEqual(0, list.Count));

            Specs +=
                "When I add an item to list"
                    .Arrange<List<int>>()
                    .With(new { a = 1 })
                    .ActOn((list, data) => list.Add(data.a))
                    .Assert("it is exactly one long",
                        (list, data) => Assert.AreEqual(1, list.Count));

            Specs +=
                "Test-Context less using with"
                    .Arrange()
                    .With(new { a = "a", b = "b", expect = "a,b" })
                    .With(new { a = "c", b = "d", expect = "c,d" })
                    .Act(data => string.Join(",", data.a, data.b))
                    .Assert((actual, data) => Assert.AreEqual(data.expect, actual));

            Specs +=
                "Multiple data arguments in with, and three assert styles"
                    .ArrangeNull()
                    .With("a", "b", "a,b")
                    .With("c", "d", "c,d")
                    .Act((_, a, b, expected) => string.Join(",", a, b))
                    .Assert((result, a, b, expected) => Assert.AreEqual(expected, result))
                    .Assert((result, expected) => Assert.AreEqual(expected, result))
                    .AssertEqualsExpected();

            Specs +=
                "When I add #a item to list"
                    .Arrange<List<int>>()
                    .With(new { a = 1 })
                    .With(new { a = 2 })
                    .ActOn((list, data) => list.Add(data.a))
                    .Assert("it is exactly one long",
                        (list, data) => Assert.AreEqual(1, list.Count));

            Specs +=
                "When I add #a to list"
                    .Arrange<List<int>>()
                    .With(new { a = 1 })
                    .With(new { a = 2 })
                    .ActOn((list, data) => list.Add(data.a))
                    .Assert("it is exactly one long",
                        (list, data) => Assert.AreEqual(1, list.Count))
                    .Assert("it contains #a",
                        (list, data) => Assert.AreEqual(data.a, list[0]));

            Specs +=
                "When I add #expectedLength items to list"
                    .Arrange<List<int>>()
                    .With(new { a = new[] { 1, 2, 3 }, expectedLength = 3, expectedSum = 6 })
                    .With(new { a = new[] { 4, 6, 7, 9 }, expectedLength = 4, expectedSum = 26 })
                    .ActOn((list, data) => list.AddRange(data.a))
                    .Assert("it is exactly #expectedLength long",
                        (list, data) => Assert.AreEqual(data.expectedLength, list.Count))
                    .Assert("the sum is #expectedSum",
                        (list, data) => Assert.AreEqual(data.expectedSum, list.Sum()));
        }
    }
}
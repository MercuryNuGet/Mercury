using Mercury;
using NUnit.Framework;
using System.Collections.Generic;

namespace MercuryTests.Extensions
{
    internal class ActsThatTakeVoidTests : MercurySuite
    {
        protected override void Specifications()
        {
            Specs +=
                "Data Act can take void"
                    .Arrange<List<int>>()
                    .ActOn(list => list.Add(3))
                    .Assert(list => Assert.AreEqual(1, list.Count));

            Specs +=
                "Data Act can take void"
                    .Arrange<List<int>>()
                    .With(new { a = 1 })
                    .ActOn((list, d) => list.Add(d.a))
                    .Assert((list, d) => Assert.AreEqual(1, list.Count));

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
                    .Arrange(() => new List<int>(new[] {1, 2, 3}))
                    .ActOn(list => list.Clear())
                    .Assert(list => Assert.AreEqual(0, list.Count));
        }
    }
}
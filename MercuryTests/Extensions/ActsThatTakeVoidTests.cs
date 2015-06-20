using System.Collections.Generic;
using Mercury;
using NUnit.Framework;

namespace MercuryTests.Extensions
{
    class ActsThatTakeVoidTests : Specification
    {
        protected override ISpecification[] TestCases()
        {
            return new ISpecification[]
            {
                "Dynamic Act can take void"
                    .Arrange<List<int>>()
                    .ActOn(list => list.Add(3))
                    .Assert(list => Assert.AreEqual(1, list.Count)),
                "Dynamic Act can take void"
                    .Arrange<List<int>>()
                    .With(new {a=1})
                    .ActOn((list, d) => list.Add(d.a))
                    .Assert((list, d) => Assert.AreEqual(1, list.Count)),         
                "Act version"
                    .Arrange(() => new List<int>(new []{1, 2, 3}))
                    .Act(list =>
                    {
                       list.Clear();
                       return list;
                    })
                    .Assert(list => Assert.AreEqual(0, list.Count)),
                "ActOn version"
                    .Arrange(() => new List<int>(new []{1, 2, 3}))
                    .ActOn(list => list.Clear())
                    .Assert(list => Assert.AreEqual(0, list.Count)),
            };
        }
    }
}

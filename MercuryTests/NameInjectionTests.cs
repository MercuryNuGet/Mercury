﻿using Mercury;
using NUnit.Framework;

namespace MercuryTests
{
    internal class NameInjectionTests : Specification
    {
        protected override ISpecification[] TestCases()
        {
            return new ISpecification[]
            {
                "In case of #case expect \"#expect\""
                    .Arrange()
                    .With(new {@case = "empty", s = "", d = new {}, expect = ""})
                    .With(new {@case = "no tags", s = "some string", d = new {}, expect = "some string"})
                    .Act(data => NameInjection.Inject(data.s, data.d))
                    .Assert((actual, data) => Assert.AreEqual(data.expect, actual)),
                "In case of #case expect \"#expect\""
                    .Arrange()
                    .With(new {@case = "single tag of value 1", s = "#a", d = new {a = 1}, expect = "1"})
                    .With(new {@case = "single tag of value 2", s = "#a", d = new {a = 2}, expect = "2"})
                    .Act(data => NameInjection.Inject(data.s, data.d))
                    .Assert((actual, data) => Assert.AreEqual(data.expect, actual)),
                "In case of #case expect \"#expect\""
                    .Arrange()
                    .With(new {@case = "two tags", s = "#a #b", d = new {a = 1, b = 2}, expect = "1 2"})
                    .With(new {@case = "missing data", s = "#c", d = new {a = 2, b = 3}, expect = "#c"})
                    .Act(data => NameInjection.Inject(data.s, data.d))
                    .Assert((actual, data) => Assert.AreEqual(data.expect, actual)),
                "In case of #case expect \"#expect\""
                    .Arrange()
                    .With(new {@case = "clashing tags", s = "#aa #a #aa", d = new {a = 2, aa = 3}, expect = "3 2 3"})
                    .Act(data => NameInjection.Inject(data.s, data.d))
                    .Assert((actual, data) => Assert.AreEqual(data.expect, actual)),
                "In case of #case expect \"#expect\""
                    .Arrange()
                    .With(new {@case = "string data", s = "Length of string is #Length", d = "abc", expect = "Length of string is 3"})
                    .Act(data => NameInjection.Inject(data.s, data.d))
                    .Assert((actual, data) => Assert.AreEqual(data.expect, actual))
            };
        }
    }
}
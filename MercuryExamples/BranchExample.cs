using Mercury;
using NUnit.Framework;

namespace MercuryExamples
{
    internal class BranchExample : Specification
    {
        protected override ISpecification[] TestCases()
        {
            return new[]
            {
                ":".Arrange<Sut>()
                    .ActOn(s => s.Navigate("/"))
                    .Assert(s => Assert.AreEqual("/", s.Path))
                    .Branch("Home:", a =>
                        new[]
                        {
                            a.ActOn(s => s.Navigate("Help/"))
                                .Assert("Help:", s => Assert.AreEqual("/Help/", s.Path)),
                            a.ActOn(s => s.Navigate("Models/"))
                                .Assert("Models:", s => Assert.AreEqual("/Models/", s.Path))
                                .Branch("Car:", b =>
                                    new ISpecification[]
                                    {
                                        b.ActOn(s => s.Navigate("Car1/"))
                                            .Assert("1:", s => Assert.AreEqual("/Models/Car1/", s.Path)),
                                        b.ActOn(s => s.Navigate("Car2/"))
                                            .Assert("2:", s => Assert.AreEqual("/Models/Car2/", s.Path))
                                    }),
                            a.ActOn(s => s.Navigate("Contact/"))
                                .Assert("Contact:", s => Assert.AreEqual("/Contact/", s.Path)),
                        }
                    )
            };
        }
    }

    internal class Sut
    {
        private string _path;

        public void Navigate(string s)
        {
            _path += s;
        }

        public string Path { get { return _path; } }
    }
}
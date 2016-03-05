using Mercury;
using NUnit.Framework;

namespace MercuryExamples
{
    internal class SpecByMethodExample : MercurySuite
    {
        protected override void Specifications()
        {
            Spec("Example of spec defined in method".Assert(() => Assert.AreEqual(2, 1 + 1)));

            Spec("Lets you space out tests".Assert(() => Assert.AreEqual(2, 1 + 1)));

            for (int i = 0; i < 10; i++)
            {
                Spec("And even lets you create specs dynamically #i"
                    .Arrange()
                    .With(new {i})
                    .Act(data => data.i*10)
                    .Assert((result, data) => Assert.IsTrue(result%10 == 0)));
            }
        }
    }
}
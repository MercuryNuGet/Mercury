# Mercury

[![NuGet Version](http://img.shields.io/nuget/v/Mercury.svg?style=flat)](https://www.nuget.org/packages/Mercury/)
[![NuGet Downloads](http://img.shields.io/nuget/dt/Mercury.svg?style=flat)](https://www.nuget.org/packages/Mercury/)
[![Build Status](https://travis-ci.org/MercuryNuGet/Mercury.svg)](https://travis-ci.org/MercuryNuGet/Mercury)

Fluent NUnit specification extensions that will run under NCrunch, and ReSharper test runners.

#Get started

##NuGet

```
install-package mercury -pre
```

##Inherit

Inherit from `SpecificationByMethod` or `Specification` and implement members.

With `Specification` you must call `Spec` for each new spec.

```
using Mercury;
using NUnit.Framework;

namespace MercuryExample
{
    public class MyTest : SpecificationByMethod
    {
        protected override void Cases()
        {
            Spec(/*see below on writing specs*/);
        }
    }
}
```

With `Specification` you return an empty array of `ISpecification`. In this array is where you will list your specifications.

```
using Mercury;
using NUnit.Framework;

namespace MercuryExample
{
  public class MyTest : Specification
  {
    protected override ISpecification[] TestCases()
    {
        return new ISpecification[]
        {
            /*see below on writing specs*/
        };
    }
  }
}
```

##Write specifications

You can copy and paste these examples directly into the array then run the tests with your favorite test runner.

###Single assert, no setup

Simplest spec is just a name and an assert:

```
"Simple assert".Assert(() => Assert.AreEqual(2, 1 + 1)),
```

This will emit a single unit test with the text "Simple assert" included in the test name.

###Arrange

Next most complex is an `Arrange`, this is equivalent to an NUnit `[SetUp]` and runs for each `Assert`,`With` combination (`With`'s are explained later):

```
"New List"
   .Arrange(() => new List<int>())
   .Assert("is empty", list => Assert.AreEqual(0, list.Count)),
```

Classes under test whose constructors do not take parameters can take advantage of this shorter syntax:

```
.Arrange<StringBuilder>())
```

If you do not require a test context, you can use `Arrange()` with no params or types:

```
"No context needed because acting on static method"
    .Arrange()
    .Act(() => string.Join(",", "a", "b"))
    .Assert(joined => Assert.AreEqual("a,b", joined)),
```

###Act

You can separate out the `Act` from the `Assert`. Here the act invokes `Any()` and the result is passed to the `Assert`.

```
"New List; linq says there is not any"
   .Arrange<List<int>>()
   .Act(list => list.Any())
   .Assert(any => Assert.IsFalse(any)),
```

Or more succinctly in this case:

```
"New List; linq says there is not any"
   .Arrange<List<int>>()
   .Act(list => list.Any())
   .Assert(Assert.IsFalse),
```

###ActOn

`Act` returns the result of it's method. This will not work if the method is `void`. `ActOn` is also way to keep the test context. Example:

```
"Act version"
   .Arrange(() => new List<int>(new []{1, 2, 3}))
   .Act(list =>
   {
     list.Clear();
     return list;
   })
   .Assert(list => Assert.AreEqual(0, list.Count)),
```

Can be just:

```
"ActOn version"
   .Arrange(() => new List<int>(new []{1, 2, 3}))
   .ActOn(list => list.Clear())
   .Assert(list => Assert.AreEqual(0, list.Count)),
```

###With

`With` enables you to parameterise your tests. It takes between 1 and 5 generic parameters. As you can set up anoymous, you can usually get away with just one and then you also get useful names.

```
"When I add an item to list"
   .Arrange<List<int>>()
   .With(new {a=1})
   .ActOn((list, data) => list.Add(data.a))
   .Assert("it is exactly one long",
      (list, data) => Assert.AreEqual(1, list.Count)),
```

`With` can be used in a situation without a test context using `Arrange()`

```
"Test-Context less using with"
    .Arrange()
    .With(new {a = "a", b = "b", expect = "a,b"})
    .With(new {a = "c", b = "d", expect = "c,d"})
    .Act(data => string.Join(",", data.a, data.b))
    .Assert((actual, data) => Assert.AreEqual(data.expect, actual)),
```

`With` when used with mutiple data items gives more assert styles:

```
"Multiple data arguments in with, and three assert styles"
    .ArrangeNull()
    .With("a", "b", "a,b")
    .With("c", "d", "c,d")
    .Act((_, a, b, expected) => string.Join(",", a, b))
    .Assert((result, a, b, expected) => Assert.AreEqual(expected, result))
    .Assert((result, expected) => Assert.AreEqual(expected, result))
    .AssertEqualsExpected(),
```

###Multiple Withs and parameter injection to test name

Use the `#` symbol to inject named parameters from your `With` data type.

```
"When I add #a item to list"
   .Arrange<List<int>>()
   .With(new {a=1})
   .With(new {a=2})
   .ActOn((list, data) => list.Add(data.a))
   .Assert("it is exactly one long",
      (list, data) => Assert.AreEqual(1, list.Count)),
```

This emits two tests:

```
When I add 1 item to list it is exactly one long
When I add 2 item to list it is exactly one long
```

###Multiple Withs and Asserts

The total number of tests emitted is the number of `Assert`s multiplied by the number of `With`s. This test below therefore runs 4 tests.

```
"When I add #a to list"
   .Arrange<List<int>>()
   .With(new {a=1})
   .With(new {a=2})
   .ActOn((list, data) => list.Add(data.a))
   .Assert("it is exactly one long",
      (list, data) => Assert.AreEqual(1, list.Count))
   .Assert("it contains #a",
      (list, data) => Assert.AreEqual(data.a, list[0])),
```

###Place expected values in the data

Where each `With` will generate a different expected value, include those expected values in the `With` data.

```
"When I add #expectedLength items to list"
   .Arrange<List<int>>()
   .With(new {a=new []{1,2,3},   expectedLength=3, expectedSum=6})
   .With(new {a=new []{4,6,7,9}, expectedLength=4, expectedSum=26})
   .ActOn((list, data) => list.AddRange(data.a))
   .Assert("it is exactly #expectedLength long",
      (list, data) => Assert.AreEqual(data.expectedLength, list.Count))
   .Assert("the sum is #expectedSum",
      (list, data) => Assert.AreEqual(data.expectedSum, list.Sum())),
```

###`SpecificationByMethod` advantages

```
class SpecByMethodExample : SpecificationByMethod
{
    protected override void Cases()
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
```
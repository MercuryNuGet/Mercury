# Mercury
Fluent NUnit specification extensions that will run under NCrunch, and ReSharper test runners.

#Get started

##NuGet

```
install-package mercury -pre
```

##Inherit

Inherit from `Specification` and implement members. Return an empty array of `ISpecification`. In this array is where you will list your specifications.

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

###Act

You can separate out the `Act` from the `Assert`. Here the act invokes `Any()` and the result is passed to the `Assert`.

```
"New List; linq says there is not any"
   .Arrange(() => new List<int>())
   .Act(list => list.Any())
   .Assert(any => Assert.IsFalse(any)),
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

`With` enables you to parameterise your tests. It takes a `dynamic`, so you can set up an anoymous type.

```
"When I add an item to list"
   .Arrange(() => new List<int>())
   .With(new {a=1})
   .Act((list, data) =>
   {
     list.Add(data.a);
     return list;
   })
   .Assert("it is exactly one long",
      (list, data) => Assert.AreEqual(1, list.Count)),
```

###Multiple Withs and parameter injection to test name

Use the `#` symbol to inject named parameters from your `With` data.

```
"When I add #a item to list"
   .Arrange(() => new List<int>())
   .With(new {a=1})
   .With(new {a=2})
   .Act((list, data) =>
   {
      list.Add(data.a);
      return list;
   })
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
   .Arrange(() => new List<int>())
   .With(new {a=1})
   .With(new {a=2})
   .Act((list, data) =>
   {
      list.Add(data.a);
      return list;
   })
   .Assert("it is exactly one long",
      (list, data) => Assert.AreEqual(1, list.Count))
   .Assert("and contains #a",
      (list, data) => Assert.AreEqual(data.a, list[0])),
```

###Place expected values in the data

Where each `With` will generate a different expected value, include those expected values in the `With` data.

```
"When I add #expectedLength items to list"
   .Arrange(() => new List<int>())
   .With(new {a=new []{1,2,3},   expectedLength=3, expectedSum=6})
   .With(new {a=new []{4,6,7,9}, expectedLength=4, expectedSum=26})
   .Act((list, data) =>
   {
      list.AddRange(data.a);
      return list;
   })
   .Assert("it is exactly #expectedLength long",
      (list, data) => Assert.AreEqual(data.expectedLength, list.Count))
   .Assert("and the sum is #expectedSum",
      (list, data) => Assert.AreEqual(data.expectedSum, list.Sum())),
```

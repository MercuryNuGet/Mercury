# Mercury
Fluid NUnit extensions

##Get stated

###NuGet

```
install-package Mercury -Pre
```

###Inherit

Inherit from `Specification` and implement members.

```
  internal class MyTest : Specification
  {
      protected override ISpecification[] TestCases()
      {
          return new ISpecification[]
          {

          };
      }
  }
```

###Write specs

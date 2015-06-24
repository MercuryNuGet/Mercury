// Code generated by a template
using System;
using Mercury.AssertBuilder;
namespace Mercury
{
  public interface IArrangedWithData<out TSut>
  {
     IAssertCaseBuilder<TPostAct> Act<TPostAct>(Func<TSut, TPostAct> actFunc);
     IArrangedWithData<TSut> With();
  }

  public interface IAssertCaseBuilder<out TSut>
  {
      IPostAssertCaseBuilder<TSut> Assert(Action<TSut> assertAction);
      IPostAssertCaseBuilder<TSut> Assert(string assertionTestCaseName, Action<TSut> assertAction);
  }

  public interface IPostAssertCaseBuilder<out TSut> : ISpecification,
        IAssertCaseBuilder<TSut>
  {
  }
}

// Code generated by a template
using System;
using Mercury.AssertBuilder;
namespace Mercury
{
  public interface IArrangedWithData<out TSut, TData>
  {
     IAssertCaseBuilder<TPostAct, TData> Act<TPostAct>(Func<TSut, TData, TPostAct> actFunc);
     IArrangedWithData<TSut, TData> With(TData data);
  }

  public interface IAssertCaseBuilder<out TSut, out TData>
  {
      IPostAssertCaseBuilder<TSut, TData> Assert(Action<TSut, TData> assertAction);
      IPostAssertCaseBuilder<TSut, TData> Assert(string assertionTestCaseName, Action<TSut, TData> assertAction);
  }

  public interface IPostAssertCaseBuilder<out TSut, out TData> : ISpecification,
        IAssertCaseBuilder<TSut, TData>
  {
  }
}

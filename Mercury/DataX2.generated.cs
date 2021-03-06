// Code generated by a template
using System;
using Mercury.AssertBuilder;
namespace Mercury
{
  public interface IArrangedWithData<out TSut, TData1, TData2>
  {
     IAssertCaseBuilder<TPostAct, TData1, TData2> Act<TPostAct>(Func<TSut, TData1, TData2, TPostAct> actFunc);
     IArrangedWithData<TSut, TData1, TData2> With(TData1 data1, TData2 data2);
  }

  public interface IAssertCaseBuilder<out TSut, out TData1, out TData2>
  {
      IPostAssertCaseBuilder<TSut, TData1, TData2> Assert(Action<TSut, TData1, TData2> assertAction);
      IPostAssertCaseBuilder<TSut, TData1, TData2> Assert(string assertionTestCaseName, Action<TSut, TData1, TData2> assertAction);
  }

  public interface IPostAssertCaseBuilder<out TSut, out TData1, out TData2> : ISpecification,
        IAssertCaseBuilder<TSut, TData1, TData2>
  {
  }
}

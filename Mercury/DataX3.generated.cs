// Code generated by a template
using System;

namespace Mercury {


  public interface IAssertWithDataCaseBuilder<out TSut, out TData1, out TData2, out TData3>
  {
      IPostAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3> Assert(Action<TSut, TData1, TData2, TData3> assertAction);
      IPostAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3> Assert(string assertionTestCaseName, Action<TSut, TData1, TData2, TData3> assertAction);
  }

   public interface IPostAssertWithDataCaseBuilder<out TSut, out TData1, out TData2, out TData3> : ISpecification,
        IAssertWithDataCaseBuilder<TSut, TData1, TData2, TData3>
  {
  }

  public class Data3{
  


  
  }

}

using System;

namespace Mercury
{
    public interface IStaticArranged
    {
        IAssertCaseBuilder<TPostAct> Act<TPostAct>(Func<TPostAct> actFunc);
        IStaticArrangedWithData<TData> With<TData>(TData data);
    }

    public interface IStaticArrangedWithData<TData>
    {
        IAssertWithDataCaseBuilder<TPostAct, TData> Act<TPostAct>(Func<TData, TPostAct> actFunc);
        IStaticArrangedWithData<TData> With(TData data);
    }

    public interface IAssertCaseBuilder<out TResult>
    {
        IPostAssertCaseBuilder<TResult> Assert(Action<TResult> assertAction);
        IPostAssertCaseBuilder<TResult> Assert(string assertionTestCaseName, Action<TResult> assertAction);
    }

    public interface IPostAssertCaseBuilder<out TSut> : ISpecification, IAssertCaseBuilder<TSut>
    {
    }
}
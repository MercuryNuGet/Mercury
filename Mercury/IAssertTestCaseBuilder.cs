using System;

namespace Mercury
{
    public interface IArranged<out TSut>
    {
        IAssertCaseBuilder<TPostAct> Act<TPostAct>(Func<TSut, TPostAct> actFunc);
        IArrangedWithData<TSut, TData> With<TData>(TData data);
    }

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

    public interface IArrangedWithData<out TSut, TData>
    {
        IAssertWithDataCaseBuilder<TPostAct, TData> Act<TPostAct>(Func<TSut, TData, TPostAct> actFunc);
        IArrangedWithData<TSut, TData> With(TData data);
    }

    public interface IIDataAssertCaseBuilder<out T, out TData>
    {
        ISpecification Assert(Action<T, TData> assertTestMethod);
    }

    public interface IDataStaticArrangedTest<out TData>
    {
        IIDataAssertCaseBuilder<TPostAct, TData> Act<TPostAct>(Func<TData, TPostAct> actFunc);
    }

    public interface IAssertCaseBuilder<out TResult>
    {
        IPostAssertCaseBuilder<TResult> Assert(Action<TResult> assertAction);
        IPostAssertCaseBuilder<TResult> Assert(string assertionTestCaseName, Action<TResult> assertAction);
    }

    public interface IAssertWithDataCaseBuilder<out TSut, out TData>
    {
        IPostAssertWithDataCaseBuilder<TSut, TData> Assert(Action<TSut, TData> assertAction);
        IPostAssertWithDataCaseBuilder<TSut, TData> Assert(string assertionTestCaseName, Action<TSut, TData> assertAction);
    }

    public interface IPostAssertCaseBuilder<out TSut> : ISpecification, IAssertCaseBuilder<TSut>
    {
    }

    public interface IPostAssertWithDataCaseBuilder<out TSut, out TData> : ISpecification,
        IAssertWithDataCaseBuilder<TSut, TData>
    {
    }
}
using System;

namespace Mercury
{
    public interface IArranged<out TSut>
    {
        IStaticPreAssertCaseBuilder<TPostAct> Act<TPostAct>(Func<TSut, TPostAct> actFunc);
        ISutArrangedWithData<TSut, TData> With<TData>(TData data);
    }

    public interface IStaticArranged
    {
        IStaticPreAssertCaseBuilder<TPostAct> Act<TPostAct>(Func<TPostAct> actFunc);
        IStaticArrangedWithData<TData> With<TData>(TData data);
    }

    public interface IStaticArrangedWithData<TData>
    {
        IPreAssertWithDataCaseBuilder<TPostAct, TData> Act<TPostAct>(Func<TData, TPostAct> actFunc);
        IStaticArrangedWithData<TData> With(TData data);
    }

    public interface ISutArrangedWithData<out TSut, TData>
    {
        IPreAssertWithDataCaseBuilder<TPostAct, TData> Act<TPostAct>(Func<TSut, TData, TPostAct> actFunc);
        ISutArrangedWithData<TSut, TData> With(TData data);
    }

    public interface IIDataAssertCaseBuilder<out T, out TData>
    {
        ISpecification Assert(Action<T, TData> assertTestMethod);
    }

    public interface IDataStaticArrangedTest<out TData>
    {
        IIDataAssertCaseBuilder<TPostAct, TData> Act<TPostAct>(Func<TData, TPostAct> actFunc);
    }

    public interface IStaticPreAssertCaseBuilder<out TResult>
    {
        IStaticAssertCaseBuilder<TResult> Assert(Action<TResult> assertAction);
        IStaticAssertCaseBuilder<TResult> Assert(string assertionTestCaseName, Action<TResult> assertAction);
    }

    public interface IStaticAssertCaseBuilder<out TResult> : ISpecification, IStaticPreAssertCaseBuilder<TResult>
    {
    }

    public interface IPreAssertWithDataCaseBuilder<out TSut, out TData>
    {
        IAssertWithDataCaseBuilder<TSut, TData> Assert(Action<TSut, TData> assertAction);
        IAssertWithDataCaseBuilder<TSut, TData> Assert(string assertionTestCaseName, Action<TSut, TData> assertAction);
    }

    public interface IAssertWithDataCaseBuilder<out TSut, out TData> : ISpecification,
        IPreAssertWithDataCaseBuilder<TSut, TData>
    {
    }

    public interface IAssertWithoutDataCaseBuilder<out TSut> : ISpecification
    {
        IAssertWithoutDataCaseBuilder<TSut> Assert(Action<TSut> assertAction);
        IAssertWithoutDataCaseBuilder<TSut> Assert(string assertionTestCaseName, Action<TSut> assertAction);
    }
}
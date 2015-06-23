using System;

namespace Mercury
{
    public interface IDataArrangedTest<out T, TData> : IAssertWithDataCaseBuilder<T, TData>
    {
        IDataArrangedTest<T, TData> With(TData data);
        IAssertWithDataCaseBuilder<TResult, TData> Act<TResult>(Func<T, TData, TResult> actFunc);
    }

    public interface IAssertCaseBuilder<out T> : ISpecification
    {
        IAssertCaseBuilder<T> Assert(Action<T> assertTestMethod);
        IAssertCaseBuilder<T> Assert(string assertionTestCaseName, Action<T> assertTestMethod);
        IDataArrangedTest<T, TData> With<TData>(TData data);
    }

    public interface IArranged<out T> : IAssertCaseBuilder<T>
    {
        IAssertCaseBuilder<TResult> Act<TResult>(Func<T, TResult> actFunc);
    }


    public interface IStaticArranged : IStaticAssertCaseBuilder
    {
        IStaticAssertCaseBuilder<TPostAct> Act<TPostAct>(Func<TPostAct> actFunc);
        IStaticArrangedWithData<TData> With<TData>(TData data);
    }

    public interface IStaticArrangedWithData<TData>
    {
        IAssertWithDataCaseBuilder<TPostAct, TData> Act<TPostAct>(Func<TData, TPostAct> actFunc);
        IStaticArrangedWithData<TData> With(TData data);
    }

    public interface IIDataAssertCaseBuilder<out T, out TData>
    {
        ISpecification Assert(Action<T, TData> assertTestMethod);
    }

    public interface IDataStaticArrangedTest<out TData>
    {
        IIDataAssertCaseBuilder<TPostAct, TData> Act<TPostAct>(Func<TData, TPostAct> actFunc);
    }

    public interface IStaticAssertCaseBuilder
    {
    }

    public interface IStaticAssertCaseBuilder<out TResult> : ISpecification
    {
        IStaticAssertCaseBuilder<TResult> Assert(Action<TResult> assertAction);
        IStaticAssertCaseBuilder<TResult> Assert(string assertionTestCaseName, Action<TResult> assertAction);
    }

    public interface IAssertWithDataCaseBuilder<out TSut, out TData> : ISpecification
    {
        IAssertWithDataCaseBuilder<TSut, TData> Assert(Action<TSut, TData> assertAction);
        IAssertWithDataCaseBuilder<TSut, TData> Assert(string assertionTestCaseName, Action<TSut, TData> assertAction);
    }

    public interface IAssertWithoutDataCaseBuilder<out TSut> : ISpecification
    {
        IAssertWithoutDataCaseBuilder<TSut> Assert(Action<TSut> assertAction);
        IAssertWithoutDataCaseBuilder<TSut> Assert(string assertionTestCaseName, Action<TSut> assertAction);
    }
}
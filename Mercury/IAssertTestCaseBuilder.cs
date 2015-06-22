using System;

namespace Mercury
{
    public interface IDataAssertCaseBuilder<out T, out TData> : ISpecification
    {
        IDataAssertCaseBuilder<T, TData> Assert(Action<T, TData> assertMethod);

        IDataAssertCaseBuilder<T, TData> Assert(string assertionTestCaseName, Action<T, TData> assertMethod);
    }

    public interface IDataArrangedTest<out T, TData> : IDataAssertCaseBuilder<T, TData>
    {
        IDataArrangedTest<T, TData> With(TData data);
        IDataAssertCaseBuilder<TResult, TData> Act<TResult>(Func<T, TData, TResult> actFunc);
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
        IStaticAssertCaseBuilder<TResult> Act<TResult>(Func<TResult> actFunc);
    }

    public interface IStaticAssertCaseBuilder
    {
    }

    public interface IStaticAssertCaseBuilder<out TResult> : ISpecification
    {
        IStaticAssertCaseBuilder<TResult> Assert(Action<TResult> assertAction);
        IStaticAssertCaseBuilder<TResult> Assert(string assertionTestCaseName, Action<TResult> assertAction);
    }
}
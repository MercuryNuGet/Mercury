using System;

namespace Mercury
{
    public interface IParamertizedDynamicAssertCaseBuilder<out T, TData> : ISpecification
    {
        IParamertizedDynamicAssertCaseBuilder<T, TData> Assert(Action<T, TData> dynamicAssertMethod);

        IParamertizedDynamicAssertCaseBuilder<T, TData> Assert(string assertionTestCaseName,
            Action<T, TData> dynamicAssertMethod);
    }

    public interface IParamertizedDynamicArrangedTest<out T, TData> : IParamertizedDynamicAssertCaseBuilder<T, TData>
    {
        IParamertizedDynamicArrangedTest<T, TData> With(TData data);
        IParamertizedDynamicAssertCaseBuilder<TResult, TData> Act<TResult>(Func<T, TData, TResult> actFunc);
    }

    public interface IAssertCaseBuilder<out T> : ISpecification
    {
        IAssertCaseBuilder<T> Assert(Action<T> assertTestMethod);
        IAssertCaseBuilder<T> Assert(string assertionTestCaseName, Action<T> assertTestMethod);
        IParamertizedDynamicArrangedTest<T, TData> With<TData>(TData data);
    }

    public interface IArranged<out T> : IAssertCaseBuilder<T>
    {
        IAssertCaseBuilder<TResult> Act<TResult>(Func<T, TResult> actFunc);
    }
}
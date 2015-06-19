using System;

namespace Mercury
{
    public interface IParamertizedDynamicAssertCaseBuilder<out T> : ISpecification
    {
        IParamertizedDynamicArrangedTest<T> Assert(Action<T, dynamic> dynamicAssertMethod);

        IParamertizedDynamicArrangedTest<T> Assert(string assertionTestCaseName,
            Action<T, dynamic> dynamicAssertMethod);
    }

    public interface IParamertizedDynamicArrangedTest<out T> : IParamertizedDynamicAssertCaseBuilder<T>
    {
        IParamertizedDynamicArrangedTest<T> With(dynamic data);
        IParamertizedDynamicAssertCaseBuilder<TResult> Act<TResult>(Func<T, dynamic, TResult> actFunc);
    }

    public interface IAssertQuickSilverCaseBuilder<out T> : ISpecification
    {
        IAssertQuickSilverCaseBuilder<T> Assert(Action<T> assertTestMethod);
        IAssertQuickSilverCaseBuilder<T> Assert(string assertionTestCaseName, Action<T> assertTestMethod);
        IParamertizedDynamicArrangedTest<T> With(dynamic data);
    }

    public interface IArrangedQuickSilver<out T> : IAssertQuickSilverCaseBuilder<T>
    {
        IAssertQuickSilverCaseBuilder<TResult> Act<TResult>(Func<T, TResult> actFunc);
    }
}
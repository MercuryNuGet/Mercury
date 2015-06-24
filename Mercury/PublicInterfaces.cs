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
        IAssertCaseBuilder<TPostAct, TData> Act<TPostAct>(Func<TData, TPostAct> actFunc);
        IStaticArrangedWithData<TData> With(TData data);
    }
}
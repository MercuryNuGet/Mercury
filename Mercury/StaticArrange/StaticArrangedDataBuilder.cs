using System;
using System.Collections.Generic;
using Mercury.AssertBuilder;

namespace Mercury.StaticArrange
{
    internal sealed class StaticArrangedDataBuilder<TData> : IStaticArrangedWithData<TData>, IDataSuite<TData>
    {
        private readonly ISuite _suite;
        private readonly List<TData> _data = new List<TData>();

        public StaticArrangedDataBuilder(ISuite suite)
        {
            _suite = suite;
        }

        public IAssertWithDataCaseBuilder<TPostAct, TData> Act<TPostAct>(Func<TData, TPostAct> actFunc)
        {
            return new StaticDataPreAssertBuilder<TPostAct, TData>(actFunc, this);
        }

        public IStaticArrangedWithData<TData> With(TData data)
        {
            _data.Add(data);
            return this;
        }

        public string SuiteName
        {
            get { return _suite.SuiteName; }
        }

        public IEnumerable<TData> Data
        {
            get { return _data; }
        }
    }
}
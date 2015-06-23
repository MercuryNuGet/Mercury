using System;
using System.Collections.Generic;
using Mercury.AssertBuilder;

namespace Mercury.Arrange
{
    internal sealed class ArrangedDataBuilder<TSut, TData> : ISutArrangedWithData<TSut, TData>, IDataSuite<TData>
    {
        private readonly ISuite _suite;
        private readonly Func<TSut> _arrangeFunc;
        private readonly List<TData> _data = new List<TData>();

        public ArrangedDataBuilder(ISuite suite, Func<TSut> arrangeFunc)
        {
            _suite = suite;
            _arrangeFunc = arrangeFunc;
        }

        public IPreAssertWithDataCaseBuilder<TPostAct, TData> Act<TPostAct>(Func<TSut, TData, TPostAct> actFunc)
        {
            return new StaticDataPreAssertBuilder<TPostAct, TData>(
                data =>
                {
                    var arranged = _arrangeFunc();
                    return actFunc(arranged, data);
                }, this);
        }

        public ISutArrangedWithData<TSut, TData> With(TData data)
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
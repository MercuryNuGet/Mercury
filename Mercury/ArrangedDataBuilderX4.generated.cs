// Code generated by a template
using System;
using Mercury.AssertBuilder;
using System.Collections.Generic;

namespace Mercury.Arrange
{
	internal sealed class ArrangedDataBuilder<TSut, TData1, TData2, TData3, TData4> : IArrangedWithData<TSut, TData1, TData2, TData3, TData4>, IDataSuite<Tuple<TData1, TData2, TData3, TData4>>
    {
        private readonly ISuite _suite;
        private readonly Func<TSut> _arrangeFunc;
        private readonly List<Tuple<TData1, TData2, TData3, TData4>> _data = new List<Tuple<TData1, TData2, TData3, TData4>>();

        public ArrangedDataBuilder(ISuite suite, Func<TSut> arrangeFunc)
        {
            _suite = suite;
            _arrangeFunc = arrangeFunc;
        }

        public IAssertCaseBuilder<TPostAct, TData1, TData2, TData3, TData4> Act<TPostAct>(Func<TSut, TData1, TData2, TData3, TData4, TPostAct> actFunc)
        {
            return new PreAssertBuilder<TPostAct, TData1, TData2, TData3, TData4>(
			    this,
                (data1, data2, data3, data4) =>
                {
                    var arranged = _arrangeFunc();
                    return actFunc(arranged, data1, data2, data3, data4);
                });
        }

        public IArrangedWithData<TSut, TData1, TData2, TData3, TData4> With(TData1 data1, TData2 data2, TData3 data3, TData4 data4)
        {
            _data.Add(Tuple.Create(data1, data2, data3, data4));
            return this;
        }

        public string SuiteName
        {
            get { return _suite.SuiteName; }
        }

        public IEnumerable<Tuple<TData1, TData2, TData3, TData4>> Data
        {
            get { return _data; }
        }
    }
    }

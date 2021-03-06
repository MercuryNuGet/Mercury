// Code generated by a template
using System;
using Mercury.AssertBuilder;
using System.Collections.Generic;

namespace Mercury.Arrange
{
	internal sealed class ArrangedDataBuilder<TSut, TData1, TData2, TData3, TData4, TData5> : IArrangedWithData<TSut, TData1, TData2, TData3, TData4, TData5>, IDataSuite<Tuple<TData1, TData2, TData3, TData4, TData5>>
    {
        private readonly ISuite _suite;
        private readonly Func<TSut> _arrangeFunc;
        private readonly List<Tuple<TData1, TData2, TData3, TData4, TData5>> _data = new List<Tuple<TData1, TData2, TData3, TData4, TData5>>();

        public ArrangedDataBuilder(ISuite suite, Func<TSut> arrangeFunc)
        {
            _suite = suite;
            _arrangeFunc = arrangeFunc;
        }

        public IAssertCaseBuilder<TPostAct, TData1, TData2, TData3, TData4, TData5> Act<TPostAct>(Func<TSut, TData1, TData2, TData3, TData4, TData5, TPostAct> actFunc)
        {
            return new PreAssertBuilder<TPostAct, TData1, TData2, TData3, TData4, TData5>(
			    this,
                (data1, data2, data3, data4, data5) =>
                {
                    var arranged = _arrangeFunc();
                    return actFunc(arranged, data1, data2, data3, data4, data5);
                });
        }

        public IArrangedWithData<TSut, TData1, TData2, TData3, TData4, TData5> With(TData1 data1, TData2 data2, TData3 data3, TData4 data4, TData5 data5)
        {
            _data.Add(Tuple.Create(data1, data2, data3, data4, data5));
            return this;
        }

        public string SuiteName
        {
            get { return _suite.SuiteName; }
        }

        public IEnumerable<Tuple<TData1, TData2, TData3, TData4, TData5>> Data
        {
            get { return _data; }
        }
    }
    }

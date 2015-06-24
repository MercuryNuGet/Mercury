﻿
// Code generated by a template
using System;
using Mercury.AssertBuilder;
using System.Collections.Generic;

namespace Mercury {

    public interface IArrangedTORENAME<out TSut>
    {
        IAssertCaseBuilder<TPostAct> Act<TPostAct>(Func<TSut, TPostAct> actFunc);
	    IArrangedWithData<TSut, TData1> With<TData1>(TData1 data1);
	    IArrangedWithData<TSut, TData1, TData2> With<TData1, TData2>(TData1 data1, TData2 data2);
	    IArrangedWithData<TSut, TData1, TData2, TData3> With<TData1, TData2, TData3>(TData1 data1, TData2 data2, TData3 data3);
	    IArrangedWithData<TSut, TData1, TData2, TData3, TData4> With<TData1, TData2, TData3, TData4>(TData1 data1, TData2 data2, TData3 data3, TData4 data4);
	    IArrangedWithData<TSut, TData1, TData2, TData3, TData4, TData5> With<TData1, TData2, TData3, TData4, TData5>(TData1 data1, TData2 data2, TData3 data3, TData4 data4, TData5 data5);
    }

}

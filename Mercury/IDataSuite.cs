using System.Collections.Generic;

namespace Mercury
{
    internal interface IDataSuite<out TData> : ISuite
    {
        IEnumerable<TData> Data { get; }
    }
}
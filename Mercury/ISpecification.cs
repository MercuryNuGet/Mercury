using System.Collections.Generic;

namespace Mercury
{
    public interface ISpecification
    {
        /// <summary>
        ///     Cause the specification to emit runnable tests. Names can clash.
        /// </summary>
        /// <returns>Runnable tests</returns>
        IEnumerable<ISingleRunnableTestCase> EmitAllRunnableTests();
    }
}
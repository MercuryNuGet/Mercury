using System.Collections.Generic;

namespace Mercury
{
    public interface ISpecification
    {
        IEnumerable<ISingleRunnableTestCase> GetAll();
    }
}

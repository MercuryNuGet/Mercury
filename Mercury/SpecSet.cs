using System.Collections.Generic;

namespace Mercury
{
    public sealed class SpecSet
    {
        private readonly List<ISpecification> _specs;

        public SpecSet()
        {
            _specs = new List<ISpecification>();
        }

        private SpecSet(List<ISpecification> specs)
        {
            _specs = specs;
        }

        public static SpecSet operator +(SpecSet set, ISpecification spec)
        {
            return new SpecSet(new List<ISpecification>(set._specs) { spec });
        }

        internal ISpecification[] ToArray()
        {
            return _specs.ToArray();
        }
    }
}
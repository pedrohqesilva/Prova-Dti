using System;
using System.Linq.Expressions;

namespace Domain.Specifications.Bases
{
    public class NullSpecification<T> : SpecificationBuilder<T>
    {
        public override Expression<Func<T, bool>> Predicate { get; }

        public NullSpecification()
        {
        }
    }
}
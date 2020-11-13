﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Specifications.Bases
{
    public class NotSpecification<T> : SpecificationBuilder<T>
    {
        private readonly ISpecification<T> _left;

        public override Expression<Func<T, bool>> Predicate
        {
            get
            {
                return Not(_left.Predicate);
            }
        }

        public NotSpecification(ISpecification<T> left)
        {
            _left = left ?? throw new ArgumentNullException(nameof(left));
        }

        private static Expression<Func<T, bool>> Not(Expression<Func<T, bool>> left)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            var notExpression = Expression.Not(left.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(notExpression, left.Parameters.Single());
            return lambda;
        }
    }
}
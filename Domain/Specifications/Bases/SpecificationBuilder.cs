﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Domain.Interfaces.Repositories.Bases;
using Domain.Specifications.Bases.Exceptions;

namespace Domain.Specifications.Bases
{
    public abstract class SpecificationBuilder<T> : ISpecification<T>
    {
        public abstract Expression<Func<T, bool>> Predicate { get; }

        protected SpecificationBuilder()
        {
        }

        public static SpecificationBuilder<T> Create()
        {
            return new NullSpecification<T>();
        }

        public bool IsSatisfiedBy(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (Predicate == null)
            {
                throw new InvalidSpecificationException("Predicate cannot be null");
            }

            var predicate = Predicate.Compile();
            return predicate(entity);
        }

        public T SatisfyingItemFrom(IQueryable<T> query)
        {
            return Prepare(query).SingleOrDefault();
        }

        public IQueryable<T> SatisfyingItemsFrom(IQueryable<T> query)
        {
            return Prepare(query);
        }

        public IQueryable<T> Prepare(IQueryable<T> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (Predicate == null)
            {
                throw new InvalidSpecificationException("Predicate cannot be null");
            }

            var q = query.Where(Predicate);
            return q;
        }

        public ISpecification<T> InitEmpty()
        {
            return new NullSpecification<T>();
        }

        public ISpecification<T> And(ISpecification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }

        public ISpecification<T> And(Expression<Func<T, bool>> right)
        {
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return new AndSpecification<T>(this, new ExpressionSpecification<T>(right));
        }

        public ISpecification<T> Or(ISpecification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }

        public ISpecification<T> Or(Expression<Func<T, bool>> right)
        {
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return new OrSpecification<T>(this, new ExpressionSpecification<T>(right));
        }

        public ISpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }
    }
}
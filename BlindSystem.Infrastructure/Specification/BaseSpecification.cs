using BlindSystem.Domain.Entities;
using BlindSystem.Domain.ISpecifications;
using System.Linq.Expressions;

namespace BlindSystem.Infrastructure.Specification
{
    public class BaseSpecification<T> : ISpecifications<T> where T : BaseEntity
    {
        public BaseSpecification()
        {
        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Include { get; set; }
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }

        public bool IsPaginationEnabled { get; set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Include.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDesc = orderByDescExpression;
        }

        protected void ApplyPagination(int skip, int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }
    }
}


using BlindSystem.Domain.Entities;
using BlindSystem.Domain.ISpecifications;
using Microsoft.EntityFrameworkCore;

namespace BlindSystem.Infrastructure.Specification
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        //needCreate method 

        public static IQueryable<TEntity> GetQuerey(IQueryable<TEntity> WorkonEntity, ISpecifications<TEntity> spec)
        {
            var query = WorkonEntity;

            //check if spec need to FilterEntity
            if (spec.Criteria is not null)
            {

                query = query.Where(spec.Criteria);
            }

            // 2. Ordering
            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDesc is not null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }



            // Pagination logic
            if (spec.IsPaginationEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Include.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));



            return query;

        }
    }
}

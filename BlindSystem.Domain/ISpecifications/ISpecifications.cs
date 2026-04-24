using BlindSystem.Domain.Entities;
using System.Linq.Expressions;

namespace BlindSystem.Domain.ISpecifications
{
    public interface ISpecifications<T> where T : BaseEntity
    {
        //Write frist step at Archecture at Specification Patteren 

        //Statment Represent Where

        public Expression<Func<T, bool>> Criteria { get; set; } // => where

        public List<Expression<Func<T, object>>> Include { get; set; }
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        bool IsPaginationEnabled { get; }
    }
}

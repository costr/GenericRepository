using System;
using System.Linq.Expressions;

namespace Repository.Interfaces {
    public interface ISaveableInt<TEntity> where TEntity: class {
        int Id { get; set; }
        Expression<Func<TEntity, object>>[] DefaultIncludes { get; }
    }
}

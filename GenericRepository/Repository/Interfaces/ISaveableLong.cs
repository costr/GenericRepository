using System;
using System.Linq.Expressions;

namespace Repository.Interfaces {
    public interface ISaveableLong<TEntity> where TEntity:class {
        long Id { get; set; }
        Expression<Func<TEntity, object>>[] DefaultIncludes { get; }
    }
}

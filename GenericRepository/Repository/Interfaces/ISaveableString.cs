using System;
using System.Linq.Expressions;

namespace Repository.Interfaces {
    public interface ISaveableString<TEntity> where TEntity : class {
        string Id { get; set; }
        Expression<Func<TEntity, object>>[] DefaultIncludes { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Repository.Interfaces;

namespace Repository.Generics {
    public class RepositoryString<TEntity> : RepositoryBase where TEntity : class, ISaveableString<TEntity> {
        public RepositoryString(DbContext ctx) : base(ctx){}
        #region Non-Async
        public TEntity GetById(string id, bool includeDefaults = false, params Expression<Func<TEntity, object>>[] includes) {
            var query = GetByIncludes(_entities.Set<TEntity>(), includes, includeDefaults);
            return query.SingleOrDefault(m => m.Id == id);
        }
        public TEntity GetBy(Expression<Func<TEntity, bool>> predicate, bool includeDefaults = false, params Expression<Func<TEntity, object>>[] includes) {
            var query = GetByIncludes(GetWhere(predicate), includes, includeDefaults);
            return query.SingleOrDefault();
        }
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool includeDefaults = false, params Expression<Func<TEntity, object>>[] includes) {
            var query = GetByIncludes(GetWhere(predicate), includes, includeDefaults);
            return query.ToList();
        }
        public List<TEntity> GetAll(bool includeDefaults = false, params Expression<Func<TEntity, object>>[] includes) {
            var query = GetByIncludes(_entities.Set<TEntity>(), includes, includeDefaults);
            return query.ToList();
        }
        public bool EntityExists(string id) {
            return _entities.Set<TEntity>().Any(t => t.Id == id);
        }
        public TEntity AddOrUpdateAndSave(TEntity entity) {
            var result = GetById(entity.Id);
            if (result == null) {
                _entities.Set<TEntity>().Add(entity);
            }
            else {
                _entities.Entry(result).CurrentValues.SetValues(entity);
            }
            Save();
            return entity;
        }
        public void AddOrUpdate(TEntity entity) {
            var result = GetById(entity.Id);
            if (result == null) {
                _entities.Set<TEntity>().Add(entity);
            }
            else {
                _entities.Entry(result).CurrentValues.SetValues(entity);
            }
        }

        public void Delete(string id) {
            var result = GetById(id);
            if (result == null) {
                throw new ObjectNotFoundException();
            }
            //do the removal here
            _entities.Set<TEntity>().Remove(result);
            Save();

        }

        #region Private Methods
        private static IQueryable<TEntity> GetByIncludes(IQueryable<TEntity> query, Expression<Func<TEntity, object>>[] includes, bool includeDefaults) {
            if (includeDefaults) {
                query = query.Include(m => m.DefaultIncludes);
            }
            if (includes == null || !includes.Any()) return query;
            query = includes.Aggregate(query, (current, expression) => current.Include(expression));
            return query;
        }
        private IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate) {
            return _entities.Set<TEntity>().Where(predicate);
        }
        #endregion

        #endregion

        #region Async
        public Task<TEntity> GetByIdAsync(string id, bool includeDefaults = false, params Expression<Func<TEntity, object>>[] includes) {
            var query = GetByIncludes(_entities.Set<TEntity>(), includes, includeDefaults);
            return query.SingleOrDefaultAsync(m => m.Id == id);
        }
        public Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> predicate, bool includeDefaults = false, params Expression<Func<TEntity, object>>[] includes) {
            var query = GetByIncludes(GetWhere(predicate), includes, includeDefaults);
            return query.SingleOrDefaultAsync();
        }
        public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, bool includeDefualts = false, params Expression<Func<TEntity, object>>[] includes) {
            var query = GetByIncludes(GetWhere(predicate), includes, includeDefualts);
            return query.ToListAsync();
        }
        public Task<List<TEntity>> GetAllAsync(bool includeDefaults = false, params Expression<Func<TEntity, object>>[] includes) {
            var query = GetByIncludes(_entities.Set<TEntity>(), includes, includeDefaults);
            return query.ToListAsync();
        }
        public Task<bool> EntityExistsAsync(string id) {
            return _entities.Set<TEntity>().AnyAsync(t => t.Id == id);
        }
        public async Task<TEntity> AddOrUpdateAndSaveAsync(TEntity entity) {
            var result = await GetByIdAsync(entity.Id);
            if (result == null) {
                _entities.Set<TEntity>().Add(entity);
            }
            else {
                _entities.Entry(result).CurrentValues.SetValues(entity);
            }
            await SaveAsync();
            return entity;
        }
        public Task DeleteAsync(string id) {
            var result = GetById(id);
            if (result == null) {
                throw new ObjectNotFoundException();
            }
            //do the removal here
            _entities.Set<TEntity>().Remove(result);
            return SaveAsync();

        }
        #endregion
    }
}

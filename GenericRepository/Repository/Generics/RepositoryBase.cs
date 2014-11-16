using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Repository.Generics {
    public abstract class RepositoryBase : IDisposable {
        protected DbContext _entities { get; set; }

        protected RepositoryBase(DbContext ctx){
            _entities = ctx;
        }

        protected void Save(){
            _entities.SaveChanges();
        }

        protected Task SaveAsync(){
            return _entities.SaveChangesAsync();
        }

        public void Dispose(){
            _entities.Dispose();
            
        }
    }
}

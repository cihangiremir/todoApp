using Core.DataAccess.Couchbase.Concrete;
using Core.Entities;
using Core.Utilities.IoC;
using Couchbase;
using Couchbase.Configuration.Client;
using Couchbase.Core;
using Couchbase.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess.Couchbase.Abstract
{
    public abstract class CouchbaseRepositoryBase<T, TId> : ICouchbaseRepository<T, TId> where T : BaseEntity, IEntity<TId>
    {
        private readonly CouchbaseSettings _couchbaseSettings;

        private ICluster _cluster;
        private IBucket _bucket;
        private IBucketContext _context;
        public CouchbaseRepositoryBase()
        {
            _couchbaseSettings = ServiceTool.ServiceProvider.GetService<IConfiguration>().GetSection("CouchbaseSettings").Get<CouchbaseSettings>();
            _cluster = new Cluster(new ClientConfiguration
            {
                Servers = new List<Uri> { new Uri(_couchbaseSettings.Host) }
            });
            _cluster.Authenticate(_couchbaseSettings.Username, _couchbaseSettings.Password);

            _bucket = _cluster.OpenBucket("todo");
            _context = new BucketContext(_bucket);
        }
        public async Task Add(T entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedDate = DateTime.Now;
            var document = new Document<T> { Id = entity.Id.ToString(), Content = entity };
            await _bucket.UpsertAsync(document);
        }

        public async Task<int> Count(Expression<Func<T, bool>> filter = null)
        {
            var query = _context.Query<T>().Where(t => t.Type == typeof(T).Name).Where(filter).Count();
            return await Task.FromResult(query);
        }

        public async Task Delete(T entity)
        {
            entity.DeletedDate = DateTime.Now;
            await _bucket.RemoveAsync(new Document<T> { Id = entity.Id.ToString(), Content = entity, });
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            var query = _context.Query<T>().Where(t => t.Type == typeof(T).Name).Where(filter);
            var result = query.SingleOrDefault();
            return await Task.FromResult(result);
        }

        public async Task<IList<T>> GetList(Expression<Func<T, bool>> filter = null)
        {
            var query = filter == null ? _context.Query<T>().Where(t => t.Type == typeof(T).Name) : _context.Query<T>().Where(t => t.Type == typeof(T).Name).Where(filter);
            return await Task.FromResult(query.ToList());
        }

        public async Task Update(T entity)
        {
            entity.ModifiedDate = DateTime.Now;
            await _bucket.ReplaceAsync(entity.Id.ToString(), entity);
        }
    }
}

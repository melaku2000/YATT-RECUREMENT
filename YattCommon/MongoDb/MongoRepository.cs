using MongoDB.Driver;
using System.Linq.Expressions;

namespace YattCommon.MongoDb
{
    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly IMongoCollection<T> _collection;
        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

        public MongoRepository(IMongoDatabase database,string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }
        public async Task<T> CreateAsync(T entity)
        {
            if(entity==null) throw new ArgumentNullException(nameof(entity));

            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(a => a.Id, id);

            await _collection.DeleteOneAsync(filter);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await _collection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate).ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(a => a.Id, id);

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate).FirstOrDefaultAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if(entity==null) throw new ArgumentNullException(nameof(entity));
            
            FilterDefinition<T> filter = filterBuilder.Eq(a => a.Id, entity.Id);
            
            await _collection.ReplaceOneAsync(filter, entity);

            return entity;
        }
    }
}

/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Threading.Tasks;
using Dolittle.ReadModels;
using MongoDB.Driver;

namespace Dolittle.ReadModels.MongoDB
{
    /// <summary>
    /// Represents an implementation of <see cref="IReadModelRepositoryFor{T}"/> for MongoDB
    /// </summary>
    public class AsyncReadModelRepositoryFor<T> : IAsyncReadModelRepositoryFor<T> where T : Dolittle.ReadModels.IReadModel
    {
        readonly string _collectionName = typeof(T).Name;
        readonly Configuration _configuration;
        readonly IMongoCollection<T> _collection;

        /// <summary>
        /// Initializes a new instance of <see cref="ReadModelRepositoryFor{T}"/>
        /// </summary>
        /// <param name="configuration"><see cref="Configuration"/> to use</param>
        public AsyncReadModelRepositoryFor(Configuration configuration)
        {
            _configuration = configuration;
            _collection = configuration.Database.GetCollection<T>(_collectionName);
        }

        /// <inheritdoc/>
        public IQueryable<T> Query => _collection.AsQueryable<T>();

        /// <inheritdoc/>
        public async Task Delete(T readModel)
        {
            var objectId = readModel.GetObjectIdFrom();
            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", objectId));
        }

        /// <inheritdoc/>
        public async Task<T> GetById(object id)
        {
            var objectId = id.GetIdAsBsonValue();
            var result = await _collection.FindAsync(Builders<T>.Filter.Eq("_id", objectId));
            return result.FirstOrDefault();
        }

        /// <inheritdoc/>
        public async Task Insert(T readModel)
        {
            await _collection.InsertOneAsync(readModel);
        }

        /// <inheritdoc/>
        public async Task Update(T readModel)
        {
            var id = readModel.GetObjectIdFrom();

            var filter = Builders<T>.Filter.Eq("_id", id);
            await _collection.ReplaceOneAsync(filter, readModel, new UpdateOptions() { IsUpsert = true });
        }
    }
}
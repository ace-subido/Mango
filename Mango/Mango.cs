namespace Mango
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Linq.Expressions;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.Linq;

    /// <summary>
    /// class to wrap up your objects for Mongo Happiness
    /// </summary>
    [BsonIgnoreExtraElements(Inherited = true)]
    public abstract class MangoModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }

    /// <summary>
    /// wrapper repository class that serves your objects to the MongoDB
    /// </summary>
    /// 
    public abstract class MangoRepository<T> where T : MangoModel
    {
        private MongoCollection<T> collection;

        public MangoRepository()           
        { }

        public MangoRepository(string collectionName)
        {
            this.collection = MongoMaker.GetCollection<T>(collectionName);
        }

        public MongoCollection<T> Collection
        {
            get
            {
                return this.collection;
            }
        }

        public T GetById(string id)
        {
            return this.collection.FindOneByIdAs<T>(id);
        }

        public T GetSingle(Expression<Func<T, bool>> criteria)
        {
            return this.collection.AsQueryable<T>().Where(criteria).Single();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> criteria)
        {
            return this.collection.AsQueryable<T>().Where(criteria);
        }

        public IQueryable<T> All()
        {
            return this.collection.AsQueryable<T>();
        }

        public T Add(T entity)
        {
            this.collection.Insert<T>(entity);
            return entity;
        }

        public IEnumerable<T> Add(IEnumerable<T> entities)
        {
            this.collection.InsertBatch<T>(entities);
            return entities;
        }

        public T Update(T entity)
        {
            this.collection.Save<T>(entity);

            return entity;
        }

        public IEnumerable<T> Update(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                this.collection.Save<T>(entity);
            }

            return entities;
        }

        public void Delete(string id)
        {           
            this.collection.Remove(Query.EQ("_id", new ObjectId(id)));           
        }

        public void Delete(T entity)
        {
            this.Delete(entity.Id.ToString());
        }

        public void Delete(Expression<Func<T, bool>> criteria)
        {
            foreach (T entity in this.collection.AsQueryable<T>().Where(criteria))
            {
                this.Delete(entity.Id.ToString());
            }
        }

        public void DeleteAll()
        {
            this.collection.RemoveAll();
        }

        public long Count()
        {
            return this.collection.Count();
        }

        public bool Exists(Expression<Func<T, bool>> criteria)
        {
            return this.collection.AsQueryable<T>().Any(criteria);
        }
    }

    /// <summary>
    /// class that spawns MonogDatabase and MongoCollection objects
    /// </summary>
    internal static class MongoMaker
    {
        private static MongoDatabase GetDatabase()
        {            
            if (ConfigurationManager.ConnectionStrings.Count > 1)
            {
                var url = new MongoUrl(ConfigurationManager.ConnectionStrings[1].ConnectionString);
                var server = MongoServer.Create(url.ToServerSettings());

                return server.GetDatabase(url.DatabaseName);
            }
            throw new InvalidOperationException("Need a connection string in your app/web.config file");
        }

        public static MongoCollection<T> GetCollection<T>(string collectionName)
            where T : MangoModel
        {
            return GetDatabase().GetCollection<T>(collectionName);
        }
    }


}

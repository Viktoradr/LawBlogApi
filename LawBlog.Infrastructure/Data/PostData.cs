using LawBlog.Infrastructure.Interfaces;
using LawBlog.Infrastructure.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace LawBlog.Infrastructure.Data
{
    public class PostData : IPostData
    {
        public IMongoDatabase database;
        public IMongoCollection<PostModel> collection;

        public PostData()
        {
            var mongoClient = new MongoClient(ConfigurationManager.AppSettings["MongoDBHost"]);
            database = mongoClient.GetDatabase(ConfigurationManager.AppSettings["MongoDBName"]);
            collection = database.GetCollection<PostModel>("post");
        }

        public List<PostModel> FindAll()
        {
            return collection.AsQueryable().ToList();
        }

        public PostModel Find(ObjectId id)
        {
            return collection.AsQueryable().SingleOrDefault(x => x._id == id);
        }

        public void Create(PostModel autor)
        {
            try
            {
                collection.InsertOne(autor);
                Console.WriteLine("Post Criado");
            }
            catch (Exception ex) { throw ex; }
        }

        public void Update(string id, PostModel autor)
        {
            try
            {
                var filter = Builders<PostModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<PostModel>.Update
                    .Set("nome", autor.titulo)
                    .Set("descricao", autor.descricao);
                var result = collection.UpdateOne(filter, update);
                Console.WriteLine("Post Atualizado");
                Console.WriteLine(result);
            }
            catch (Exception ex) { throw ex; }
        }

        public void Delete(string id)
        {
            try
            {
                var result = collection.DeleteOne(Builders<PostModel>.Filter.Eq("_id", ObjectId.Parse(id)));
                Console.WriteLine("Post Deletado");
                Console.WriteLine(result);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}

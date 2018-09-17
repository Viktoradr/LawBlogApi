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
    public class AutorData : IAutorData
    {
        public IMongoDatabase database;
        public IMongoCollection<AutorModel> collection;

        public AutorData()
        {
            var mongoClient = new MongoClient(ConfigurationManager.AppSettings["MongoDBHost"]);
            database = mongoClient.GetDatabase(ConfigurationManager.AppSettings["MongoDBName"]);
            collection = database.GetCollection<AutorModel>("autor");
        }

        public List<AutorModel> FindAll()
        {
            return collection.AsQueryable().ToList();
        }

        public AutorModel Find(ObjectId id)
        {
            return collection.AsQueryable().SingleOrDefault(x => x._id == id);
        }

        public void Create(AutorModel autor)
        {
            try
            {
                collection.InsertOne(autor);
                Console.WriteLine("Autor Criado");
            }
            catch (Exception ex) { throw ex; }
        }

        public void Update(string id, AutorModel autor)
        {
            try
            {
                var filter = Builders<AutorModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<AutorModel>.Update
                    .Set("nome", autor.nome);
                var result = collection.UpdateOne(filter, update);
                Console.WriteLine("Autor Atualizado");
                Console.WriteLine(result);
            }
            catch (Exception ex) { throw ex; }
        }

        public void Delete(string id)
        {
            try
            {
                var result = collection.DeleteOne(Builders<AutorModel>.Filter.Eq("_id", ObjectId.Parse(id)));
                Console.WriteLine("Autor Deletado");
                Console.WriteLine(result);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}

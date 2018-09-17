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
    public class UsuarioData : IUsuarioData
    {
        public IMongoDatabase database;
        public IMongoCollection<UsuarioModel> collection;

        public UsuarioData()
        {
            var mongoClient = new MongoClient(ConfigurationManager.AppSettings["MongoDBHost"]);
            database = mongoClient.GetDatabase(ConfigurationManager.AppSettings["MongoDBName"]);
            collection = database.GetCollection<UsuarioModel>("usuario");
        }

        public List<UsuarioModel> FindAll()
        {
            return collection.AsQueryable().ToList();
        }

        public UsuarioModel Find(ObjectId id)
        {
            return collection.AsQueryable().SingleOrDefault(x => x._id == id);
        }

        public UsuarioModel RecuperarPorAutenticacao(string Email, string SenhaCriptografada)
        {
            try { return collection.AsQueryable().SingleOrDefault(x => x.email == Email && x.senha == SenhaCriptografada); }
            catch (Exception ex) { throw ex; }
        }

        public void Create(UsuarioModel autor)
        {
            try
            {
                collection.InsertOne(autor);
                Console.WriteLine("Usuario Criado");
            }
            catch (Exception ex) { throw ex; }
        }

        public void Update(string id, UsuarioModel autor)
        {
            try
            {
                var filter = Builders<UsuarioModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<UsuarioModel>.Update
                    .Set("nome", autor.nome);
                var result = collection.UpdateOne(filter, update);
                Console.WriteLine("Usuario Atualizado");
                Console.WriteLine(result);
            }
            catch (Exception ex) { throw ex; }
        }

        public void Delete(string id)
        {
            try
            {
                var result = collection.DeleteOne(Builders<UsuarioModel>.Filter.Eq("_id", ObjectId.Parse(id)));
                Console.WriteLine("Usuario Deletado");
                Console.WriteLine(result);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}

using LawBlog.Infrastructure.Models;
using MongoDB.Bson;
using System.Collections.Generic;

namespace LawBlog.Infrastructure.Interfaces
{
    public interface IAutorData
    {
        List<AutorModel> FindAll();
        AutorModel Find(ObjectId id);
        void Create(AutorModel autor);
        void Update(string id, AutorModel autor);
        void Delete(string id);
    }
}

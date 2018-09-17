using LawBlog.Infrastructure.Models;
using MongoDB.Bson;
using System.Collections.Generic;

namespace LawBlog.Infrastructure.Interfaces
{
    public interface IPostData
    {
        List<PostModel> FindAll();
        PostModel Find(ObjectId id);
        void Create(PostModel autor);
        void Update(string id, PostModel autor);
        void Delete(string id);
    }
}

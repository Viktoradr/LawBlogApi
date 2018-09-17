using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LawBlog.Infrastructure.Models
{
    public class AutorModel
    {
        public ObjectId _id { get; set; }
        public string nome { get; set; }
    }
}

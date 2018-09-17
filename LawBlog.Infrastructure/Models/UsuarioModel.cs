using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LawBlog.Infrastructure.Models
{
    public class UsuarioModel
    {
        public ObjectId _id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
    }
}

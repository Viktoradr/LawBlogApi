using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LawBlog.Infrastructure.Models
{
    public class PostModel
    {
        public PostModel()
        {
            this.autor = new AutorModel();
        }

        public ObjectId _id { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }

        public ObjectId usuarioId { get; set; }
        
        public AutorModel autor { get; set; }
    }
}

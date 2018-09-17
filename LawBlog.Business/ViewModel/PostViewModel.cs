using MongoDB.Bson;

namespace LawBlog.Business.ViewModel
{
    public class PostViewModel
    {
        public string id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AutorViewModel Autor { get; set; }
        public UsuarioViewModel Usuario { get; set; }
    }
}

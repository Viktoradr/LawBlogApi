using LawBlog.Business.ViewModel;
using MongoDB.Bson;

namespace LawBlog.Business.Interfaces
{
    public interface IAutenticacaoCore
    {
        bool ValidarUsuario(ObjectId _id);
        UsuarioViewModel AutenticarUsuario(LoginViewModel view);
    }
}

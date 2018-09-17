using LawBlog.Infrastructure.Models;
using MongoDB.Bson;
using System.Collections.Generic;

namespace LawBlog.Infrastructure.Interfaces
{
    public interface IUsuarioData
    {
        List<UsuarioModel> FindAll();
        UsuarioModel Find(ObjectId id);
        UsuarioModel RecuperarPorAutenticacao(string Email, string SenhaCriptografada);
        void Create(UsuarioModel autor);
        void Update(string id, UsuarioModel autor);
        void Delete(string id);
    }
}

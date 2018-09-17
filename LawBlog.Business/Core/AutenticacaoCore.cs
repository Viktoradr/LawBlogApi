using LawBlog.Business.Interfaces;
using LawBlog.Business.ViewModel;
using LawBlog.Infrastructure.Interfaces;
using LawBlog.Infrastructure.Models;
using LawBlog.Utilities.Criptografia;
using MongoDB.Bson;
using Ninject;
using System;
using System.Linq;

namespace LawBlog.Business.Core
{
    public class AutenticacaoCore : IAutenticacaoCore
    {
        #region Atributos
        private readonly IUsuarioData usuarioData;
        #endregion

        #region Construtor
        [Inject]
        public AutenticacaoCore(IUsuarioData usuarioData)
        {
            this.usuarioData = usuarioData;
        }
        #endregion

        private UsuarioViewModel MontarView(UsuarioModel usuario)
        {
            UsuarioViewModel view = new UsuarioViewModel();

            view.id = usuario._id.ToString();
            view.Nome = usuario.nome;
            view.Email = usuario.email;

            return view;
        }

        public bool ValidarUsuario(ObjectId _id)
        {
            return (!string.IsNullOrEmpty(_id.ToString()) ? usuarioData.Find(_id) != null : false);
        }

        public UsuarioViewModel AutenticarUsuario(LoginViewModel view)
        {
            var SenhaCriptografada = MD5Hash.Criptografar(string.Concat(view.Senha, view.Email));
            try
            {
                var usuario = usuarioData.RecuperarPorAutenticacao(view.Email, view.Senha); //SenhaCriptografada

                if (usuario == null) return null;
                else return MontarView(usuario);
            }
            catch (Exception) { return null; }
        }
    }
}

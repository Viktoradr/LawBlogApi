using LawBlog.Business.Interfaces;
using LawBlog.Business.ViewModel;
using LawBlog.Infrastructure.Interfaces;
using LawBlog.Infrastructure.Models;
using Ninject;
using System.Collections.Generic;
using System.Linq;

namespace LawBlog.Business.Core
{
    public class UsuarioCore : IUsuarioCore
    {
        #region Atributos
        private readonly IUsuarioData usuarioData;
        #endregion

        #region Construtor
        [Inject]
        public UsuarioCore(IUsuarioData usuarioData)
        {
            this.usuarioData = usuarioData;
        }
        #endregion

        private UsuarioViewModel MontarView(UsuarioModel usuario)
        {
            var view = new UsuarioViewModel();

            view.id = usuario._id.ToString();
            view.Nome = usuario.nome;
            view.Email = usuario.email;

            return view;
        }

        public List<UsuarioViewModel> RecuperarTodosUsuarios()
        {
            var list = new List<UsuarioViewModel>();
            var usuarios = usuarioData.FindAll();

            if(usuarios.Any())
            {
                foreach (var item in usuarios)
                {
                    list.Add(MontarView(item));
                }
            }

            return list;
        }
    }
}

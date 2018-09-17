using LawBlog.Business.Interfaces;
using LawBlog.Business.ViewModel;
using LawBlog.Infrastructure.Interfaces;
using LawBlog.Infrastructure.Models;
using Ninject;
using System.Collections.Generic;
using System.Linq;

namespace LawBlog.Business.Core
{
    public class AutorCore : IAutorCore
    {
        #region Atributos
        private readonly IAutorData autorData;
        #endregion

        #region Construtor
        [Inject]
        public AutorCore(IAutorData autorData)
        {
            this.autorData = autorData;
        }
        #endregion

        private AutorViewModel MontarView(AutorModel autor)
        {
            var view = new AutorViewModel();

            view.id = autor._id.ToString();
            view.Nome = autor.nome;

            return view;
        }

        public List<AutorViewModel> RecuperarTodosAutores()
        {
            var list = new List<AutorViewModel>();
            var autores = autorData.FindAll();

            if(autores.Any())
            {
                foreach (var item in autores)
                {
                    list.Add(MontarView(item));
                }
            }

            return list;
        }
    }
}

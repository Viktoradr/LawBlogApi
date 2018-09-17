using LawBlog.Business.Interfaces;
using LawBlog.Business.ViewModel;
using LawBlog.Infrastructure.Interfaces;
using LawBlog.Infrastructure.Models;
using MongoDB.Bson;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LawBlog.Business.Core
{
    public class PostCore : IPostCore
    {
        #region Atributos
        private readonly IPostData postData;
        private readonly IAutorData autorData;
        #endregion

        #region Construtor
        [Inject]
        public PostCore(IPostData postData, IAutorData autorData)
        {
            this.postData = postData;
            this.autorData = autorData;
        }
        #endregion

        private PostViewModel MontarView(PostModel post)
        {
            var view = new PostViewModel();

            view.id = post._id.ToString();
            view.Titulo = post.titulo;
            view.Descricao = post.descricao;

            view.Usuario = new UsuarioViewModel();
            view.Usuario.id = post.usuarioId.ToString();

            view.Autor = new AutorViewModel();
            view.Autor.id = post.autor._id.ToString();
            view.Autor.Nome = post.autor.nome;

            return view;
        }

        public List<PostViewModel> RecuperarTodosPosts()
        {
            var list = new List<PostViewModel>();
            var posts = postData.FindAll();

            if(posts.Any())
            {
                foreach (var item in posts)
                {
                    list.Add(MontarView(item));
                }
            }

            return list;
        }

        public bool NovoPost(PostViewModel post)
        {
            PostModel _post = new PostModel();

            AutorModel _autor = autorData.Find(new ObjectId(post.Autor.id));

            try
            {
                _post.titulo = post.Titulo;
                _post.descricao = post.Descricao;
                _post.autor._id = _autor._id;
                _post.autor.nome = _autor.nome;
                _post.usuarioId = new ObjectId(post.Usuario.id);

                postData.Create(_post);

                return true;
            }
            catch(Exception ex) { throw ex; }
        }
    }
}

using System;
using LawBlog.Business.Core;
using LawBlog.Business.Interfaces;
using LawBlog.Infrastructure;
using LawBlog.Infrastructure.Data;
using LawBlog.Infrastructure.Interfaces;

namespace InjectorProject
{
    public class ContainerSetup
    {
        /// <summary>
        /// Declarações
        /// </summary>
        private static Boolean configured;

        /// <summary>
        /// Verifica necessidade de reconfiguração
        /// </summary>
        public static void Reconfigure()
        {
            configured = false;
            Configure();
        }

        /// <summary>
        /// Configura o container
        /// </summary>
        public static void Configure()
        {
            if (!configured)
            {
                Container.Setup();
                configured = true;


                #region Data Binding
                Container.Bind<IUsuarioData, UsuarioData>();
                Container.Bind<IAutorData, AutorData>();
                Container.Bind<IPostData, PostData>();
                #endregion Bm Binding

                #region Core Binding
                Container.Bind<IAutenticacaoCore, AutenticacaoCore>();
                Container.Bind<IAutorCore, AutorCore>();
                Container.Bind<IUsuarioCore, UsuarioCore>();
                Container.Bind<IPostCore, PostCore>();

                #endregion BP Binding
            }
        }

        public static T Get<T>()
        {
            return ContainerSetup.Get<T>();
        }
    }
}

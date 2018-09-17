using System;
using Ninject;

namespace InjectorProject
{
    /// <summary>
    /// Container a ser utilizado para configurar e obter instâncias de classes e mock's
    /// </summary>
    /// <remarks>
    /// Os objetos obtidos serão devidamente inicializados pelo container de inversão de
    /// controle (Ninject), que preencherá automaticamente todas as dependências indicadas
    /// pelo atributo "Inject"
    /// </remarks>
    public static class Container
    {
        private static IKernel kernel;

        /// <summary>
        /// Inicializa o Container
        /// </summary>
        /// <remarks>
        /// Deve ser executado antes de qualquer outra funcionalidade ser utilizada
        /// </remarks>
        public static void Setup()
        {
            //todo:resolver o problema do InjectNonPublic = true
            //var options = new NinjectSettings {InjectNonPublic = true/*,InjectAttribute =typeof(InjectAttribute)*/, };
            //kernel = new StandardKernel(options);
            kernel = new StandardKernel();
        }

        /// <summary>
        /// Informa o tipo de objeto a ser instanciado quando determinada
        /// Interface for requisitada pelo Container
        /// </summary>
        /// <typeparam name="TInterface">O tipo da Interface requisitada</typeparam>
        /// <typeparam name="TImplementation">O tipo a ser instanciado para a Interface requisitada</typeparam>
        public static void Bind<TInterface, TImplementation>()
        {
            ValidateContainer();

            kernel.Bind<TInterface>().To(typeof(TImplementation));
        }

        /// <summary>
        /// Informa o tipo de objeto a ser instanciado quando determinada
        /// Interface for requisitada pelo Container
        /// </summary>
        /// <typeparam name="TInterface">O tipo da Interface requisitada</typeparam>
        /// <typeparam name="TImplementation">O tipo a ser instanciado para a Interface requisitada</typeparam>
        public static void Bind<TInterface, TImplementation>(bool isSigleton)
        {
            ValidateContainer();

            if (isSigleton)
            {
                kernel.Bind<TInterface>().To(typeof(TImplementation));
            }
            else
            {
                kernel.Bind<TInterface>().To(typeof(TImplementation));
            }
        }

        /// <summary>
        /// Informa o tipo de objeto a ser instanciado quando determinada
        /// Interface for requisitada pelo Container
        /// </summary>
        /// <typeparam name="TInterface">O tipo da Interface requisitada</typeparam>
        /// <typeparam name="TImplementation">O tipo a ser instanciado para a Interface requisitada</typeparam>
        /// <typeparam name="TInjected"> </typeparam>
        public static void Bind<TInterface, TImplementation, TInjected>()
        {
            ValidateContainer();
            kernel.Bind<TInterface>().To(typeof(TImplementation)).WhenInjectedInto(typeof(TInjected));
        }

        /// <summary>
        /// Informa o tipo de objeto a ser instanciado quando determinada
        /// Interface for requisitada pelo Container
        /// </summary>
        /// <typeparam name="TInterface">O tipo da Interface requisitada</typeparam>
        /// <typeparam name="TImplementation">O tipo a ser instanciado para a Interface requisitada</typeparam>
        public static void Bind<TInterface>(Func<Ninject.Activation.IContext, TInterface> method)
        {
            ValidateContainer();

            kernel.Bind<TInterface>().ToMethod(method);
        }

        /// <summary>
        /// Retorna uma instância do tipo informado, obtendo a instância
        /// do container de IoC (Ninject)
        /// </summary>
        /// <typeparam name="T">O tipo do objeto a ser retornado</typeparam>
        /// <returns>Uma instância do tipo informado</returns>
        public static T Get<T>()
        {
            ValidateContainer();

            return kernel.Get<T>();
        }

        /// <summary>
        /// Retorna uma instância do tipo informado, obtendo a instância
        /// do container de IoC (Ninject)
        /// </summary>
        /// <param name="type">tipo do objeto que deve ser criado</param>
        /// <returns>objeto criado</returns>
        public static object Get(Type type)
        {
            ValidateContainer();

            return kernel.Get(type);
        }

        /// <summary>
        /// Preenche as propriedades decoradas com o atributo "Inject" do objeto informado
        /// com as instâncias existentes no container.
        /// </summary>
        /// <param name="instance">A instância a ser preenchida</param>
        public static void Inject(object instance)
        {
            ValidateContainer();

            kernel.Inject(instance);
        }

        private static void ValidateContainer()
        {
            if (kernel == null)
            {
                throw new InvalidOperationException("O container não foi inicializado. Execute o método DependencyInjectionContainer.Setup() antes de utilizar o container");
            }
        }
    }
}

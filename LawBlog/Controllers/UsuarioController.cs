using InjectorProject;
using LawBlog.Business.Interfaces;
using LawBlog.Utilities.Api;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace LawBlog.Controllers
{
    [RoutePrefix("api/Usuario")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsuarioController : ApiController
    {
        private IUsuarioCore usuarioCore = Container.Get<IUsuarioCore>();

        [HttpGet]
        [Route("All")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage All()
        {
            var retorno = Request.CreateResponse(HttpStatusCode.OK);

            try
            {
                var list = usuarioCore.RecuperarTodosUsuarios();
                return ResponseHelpers.ResponseAPI(list, retorno);
            }
            catch (Exception ex)
            {
                var exception = ex.StackTrace;
                retorno = Request.CreateResponse(HttpStatusCode.BadRequest);
                return ResponseHelpers.ResponseAPI(false, retorno);
            }
        }
    }
}

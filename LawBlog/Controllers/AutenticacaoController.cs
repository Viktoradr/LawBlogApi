using InjectorProject;
using LawBlog.Business.Interfaces;
using LawBlog.Business.ViewModel;
using LawBlog.Utilities.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace LawBlog.Controllers
{
    [RoutePrefix("api/Auth")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AutenticacaoController : ApiController
    {
        private IAutenticacaoCore autenticacaoCore = Container.Get<IAutenticacaoCore>();

        [HttpPost]
        [Route("Login")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage Login(LoginViewModel view)
        {
            var retorno = Request.CreateResponse(HttpStatusCode.OK);

            try
            {
                var usuario = autenticacaoCore.AutenticarUsuario(view);
                return ResponseHelpers.ResponseAPI(usuario, retorno);
            }
            catch (Exception ex)
            {
                var exception = ex.StackTrace;
                retorno = Request.CreateResponse(HttpStatusCode.BadRequest);
                return ResponseHelpers.ResponseAPI("", retorno);
            }
        }
    }
}
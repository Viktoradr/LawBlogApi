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
    [RoutePrefix("api/Post")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PostController : ApiController
    {
        private IPostCore postCore = Container.Get<IPostCore>();

        [HttpGet]
        [Route("All")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage All()
        {
            var retorno = Request.CreateResponse(HttpStatusCode.OK);

            try
            {
                var list = postCore.RecuperarTodosPosts();
                return ResponseHelpers.ResponseAPI(list, retorno);
            }
            catch (Exception ex)
            {
                var exception = ex.StackTrace;
                retorno = Request.CreateResponse(HttpStatusCode.BadRequest);
                return ResponseHelpers.ResponseAPI(false, retorno);
            }
        }

        [HttpPost]
        [Route("New")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage NovoPost(PostViewModel post)
        {
            var retorno = Request.CreateResponse(HttpStatusCode.OK);

            try
            {
                var criado = postCore.NovoPost(post);
                return ResponseHelpers.ResponseAPI(criado, retorno);
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

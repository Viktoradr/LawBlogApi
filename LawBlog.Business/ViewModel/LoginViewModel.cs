using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawBlog.Business.ViewModel
{
    public class LoginViewModel
    {
        public LoginViewModel() { }

        public string id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}

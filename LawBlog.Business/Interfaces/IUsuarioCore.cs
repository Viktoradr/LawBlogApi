using LawBlog.Business.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawBlog.Business.Interfaces
{
    public interface IUsuarioCore
    {
        List<UsuarioViewModel> RecuperarTodosUsuarios();
    }
}

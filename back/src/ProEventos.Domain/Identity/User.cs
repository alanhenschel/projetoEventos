using Microsoft.AspNetCore.Identity;
using ProEventos.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Domain.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string Descricao { get; set; }
        public Titulo Titulo { get; set; }
        public Funcao Funcao { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}

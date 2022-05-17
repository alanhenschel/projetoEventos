using ProEventos.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Contratos
{
    public interface IUserPersist : IGeralPersist
    {
        Task<IEnumerable<User>> GetUsersAsyc();
        Task<User> GetUserByIdAsyc(Guid id);
        Task<User> GetUserByUsernameAsync(string username);
    }
}

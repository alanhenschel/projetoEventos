using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{
    public class UserPersistence : GeralPersistence, IUserPersist
    {
        private readonly ProEventosContext Context;

        public UserPersistence(ProEventosContext context) : base(context)
        {
            Context = context;
        }

        public async Task<User> GetUserByIdAsyc(Guid id)
        {
            return await Context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await Context.Users.SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<User>> GetUsersAsyc()
        {
            return await Context.Users.ToListAsync();
        }
    }
}

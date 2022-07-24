using Microsoft.AspNetCore.Identity;
using ProEventos.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Contratos
{
    public interface IUserService
    {
        Task<bool> UserExists(string username);
        Task<UserUpdateDto> GetUsersByUsernameAsync(string username);
        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto user, string password);
        Task<UserDto> CreateUserAsync(UserDto user);
        Task<UserUpdateDto> UpdateUser(UserUpdateDto user);
    }
}

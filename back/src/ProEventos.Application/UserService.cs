using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserPersist _userPersist;

        public UserService(UserManager<User> userManager,
                            SignInManager<User> signInManager,
                            IMapper mapper,
                            IUserPersist userPersist)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
            this._userPersist = userPersist;
            
        }
        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto user, string password)
        {
            try
            {
                var account = await this._userManager.Users.SingleOrDefaultAsync(user => user.UserName == user.UserName.ToLower());

                return await this._signInManager.CheckPasswordSignInAsync(account, password, false);
            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Erro ao tentar verificar senha, erro: {ex.Message}");
            }
        }

        public async Task<UserDto> CreateUserAsync(UserDto user)
        {
            try
            {
                var account = this._mapper.Map<User>(user);
                var result = await this._userManager.CreateAsync(account, user.Password);

                if (result.Succeeded) {
                    var userDto = this._mapper.Map<UserDto>(account);
                    return userDto;
                }
                return null;
            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Erro ao tentar criar conta, erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> GetUsersByUsernameAsync(string username)
        {
            try
            {
                var user = await this._userPersist.GetUserByUsernameAsync(username);
                if (user == null) return null;

                var userUpdateDto = this._mapper.Map<UserUpdateDto>(user);
                return userUpdateDto;
                
            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Erro ao tentar pegar usuario, erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> UpdateUser(UserUpdateDto user)
        {
            try
            {
                var account = await this._userPersist.GetUserByUsernameAsync(user.UserName);
                if (account == null) return null;

                this._mapper.Map(user, account);

                var token = await _userManager.GeneratePasswordResetTokenAsync(account);
                var result = await this._userManager.ResetPasswordAsync(account, token, user.Password);

                this._userPersist.Update<User>(account);

                if (await this._userPersist.SaveChangesAsync()) {
                    var userRetorno = await this._userPersist.GetUserByUsernameAsync(account.UserName);

                    return this._mapper.Map<UserUpdateDto>(userRetorno);
                }

                return null;
            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Erro ao tentar atualizar usuario, erro: {ex.Message}");
            }
        }

        public async Task<bool> UserExists(string username)
        {
            try
            {
                return await this._userManager.Users.AnyAsync(user => user.UserName == username.ToLower());
                
            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Erro ao tentar verificar se usuario existe, erro: {ex.Message}");
            }
        }
    }
}
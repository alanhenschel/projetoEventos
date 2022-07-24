using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Extensions;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;

namespace ProEventos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService,
                              ITokenService tokenService)
        {
            this._userService = userService;
            this._tokenService = tokenService;
        }

        [HttpGet("GetUser")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser ()
        {
            try
            {
                var userName = User.GetUserName();
                var user = await this._userService.GetUsersByUsernameAsync(userName);

                return Ok(user);
                
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar usuario. Erro: {ex.Message}");
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto user)
        {
            try
            {
                if(await this._userService.UserExists(user.Username)) {
                    return BadRequest("Usuario ja existe");
                }

                var userCreated = await this._userService.CreateUserAsync(user);
                if(userCreated != null) {
                    return Ok(userCreated);
                }

                return BadRequest("Não foi possivel");
                
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar usuario. Erro: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login (UserLoginDto login)
        {
            try
            {
                var user = await this._userService.GetUsersByUsernameAsync(login.Username);

                if (user == null) return Unauthorized("Usuario invalido");

                var result = await this._userService.CheckUserPasswordAsync(user, login.Password);

                if (!result.Succeeded) return Unauthorized();

                return Ok(new {
                    userName = user.UserName,
                    primeiroNome = user.PrimeiroNome,
                    token = this._tokenService.CreateToken(user).Result
                });
                
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar usuario. Erro: {ex.Message}");
            }
        }


        [HttpPut("UpdateUser")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdate)
        {
            try
            {
                var user = await this._userService.GetUsersByUsernameAsync(User.GetUserName());

                if (user == null) return Unauthorized("Usuario invalido");

                var userUpdated = await this._userService.UpdateUser(user);
                if(userUpdated == null) {
                    return NoContent();
                }

                return Ok(userUpdated);
                
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar usuario. Erro: {ex.Message}");
            }
        }

    }
}
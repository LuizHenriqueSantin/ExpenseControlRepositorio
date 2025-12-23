using ExpenseControl.Application.Interfaces.Repositories;
using ExpenseControl.Application.Interfaces.Services;
using ExpenseControl.Application.Models;
using ExpenseControl.Application.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if(user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Email ou Senha incorretos.");
            }

            var token = _tokenService.GenerateToken(user);

            return new AuthResponse
            {
                Token = token,
            };
        }
    }
}

using ExpenseControl.Application.Interfaces;
using ExpenseControl.Application.Interfaces.Repositories;
using ExpenseControl.Application.Interfaces.Services;
using ExpenseControl.Application.Models;
using ExpenseControl.Application.Models.Helpers;
using ExpenseControl.Application.Models.In;
using ExpenseControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuthenticationService _authenticationService;

        public UserService(IUserRepository userRepository, ICurrentUserService currentUser, IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _currentUserService = currentUser;
            _authenticationService = authenticationService;
        }

        public async Task<bool> CreateAsync(UserIn user)
        {
            var existingUser = await _userRepository.GetByEmailAsync(user.Email);
            if(existingUser != null)
            {
                throw new Exception("Email já cadastrado!");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var userEntity = new User
            {
                Email = user.Email,
                PasswordHash = passwordHash,
            };

            return await _userRepository.AddAsync(userEntity);
        }

        public async Task<bool> DeleteCurrentUserAsync()
        {
            var userId = _currentUserService.GetUserId();

            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            if(user.Email == "admin@admin.com") //Hardcoded apenas para deixar mais simples
            {
                throw new Exception("Deleção não permitida");
            }
            
            return await _userRepository.DeleteAsync(user);
        }
    }
}

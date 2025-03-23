using AutoMapper;
using IdentityBLL.Interfaces;
using IdentityBLL.Models;
using IdentityDAL.Entities;
using IdentityDAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace IdentityBLL.Services
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return users.Select(p => _mapper.Map<UserModel>(p)).ToList();
        }

        public async Task<UserModel> GetByIdAsync(Guid id)
        {
            var project = await _unitOfWork.UserRepository.GetByIdAsync(id);
            return _mapper.Map<UserModel>(project);
        }

        public async Task AddAsync(UserModel model)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(model.Password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            var mappedUser = _mapper.Map<User>(model);
            mappedUser.Password = Convert.ToHexString(hashBytes);

            await _unitOfWork.UserRepository.AddAsync(mappedUser);
            await _unitOfWork.SaveAsync();
        }

        public async Task<string> AuthorizeAsync(string email, string password)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(email);

            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            var passwordHash = Convert.ToHexString(hashBytes);

            if (user == null || passwordHash != user.Password)
            {
                throw new Exception("Incorrect UserName or Password");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.RoleName)
            };

            var token = new JwtSecurityToken(
                _configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task UpdateAsync(UserModel model)
        {
            var project = _mapper.Map<User>(model);
            await _unitOfWork.UserRepository.UpdateAsync(project);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.UserRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}

using AutoMapper;
using DevTask2.Business.ServiceInterface;
using DevTask2.DataAdapters.DBModels;
using DevTask2.Models.UserModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DevTask2.Business
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<RegisterAuthModel> _passwordHasher;

        public AuthService(IUserService userService, IConfiguration configuration, IMapper mapper, IPasswordHasher<RegisterAuthModel> passwordHasher)
        {
            _configuration = configuration;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _userService = userService;

        }

        public async Task<bool> RegisterUser(RegisterAuthModel add_User, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(add_User.UserName) || string.IsNullOrWhiteSpace(add_User.Password))
            {
                throw new ArgumentException("Username or Password are required.");
            }

            var existingUser = await _userService.GetUserByUsername(add_User.UserName, cancellationToken);

            if (existingUser != null)
            {
                throw new ArgumentException("The user is already exists.");
            }

            var hashPassword = _passwordHasher.HashPassword(add_User, add_User.Password);
            string assignedRole = "User";

            if (add_User.AdminSecurityKey != null)
            {
                var adminKey = _configuration.GetValue<string>("AdminSecurityKey:Key");
                if (add_User.AdminSecurityKey == adminKey)
                {
                    assignedRole = "Admin";
                }
                else
                {
                    throw new ArgumentException("Invalid Admin Security Key.");
                }
            }

            var newUser = new UserModel
            {
                Username = add_User.UserName,
                Password = hashPassword,
                Role = assignedRole
            };
            await _userService.AddUserAsync(newUser, cancellationToken);


            return true;
        }

        public async Task<string?> LoginUserAsync(UserAuthModel user, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            {
                throw new ArgumentException("Please enter valid user name or password.");
            }

            var existUser = await _userService.GetUserByUsername(user.Username, cancellationToken);
            if (existUser != null)
            {
                var tblUser = _mapper.Map<TblUser>(existUser);
                var passwordVerfication = new PasswordHasher<TblUser>();
                var password = passwordVerfication.VerifyHashedPassword(tblUser, tblUser.password, user.Password);
                if (password == PasswordVerificationResult.Success)
                {
                    var userModel = _mapper.Map<ViewUserModel>(tblUser);
                    var token = CreateToken(userModel);
                    return token;
                }
                else
                {
                    return null;
                }

            }
            return null;

        }


        private string CreateToken(ViewUserModel user)
        {
            if (user.UserName == null || user.Role == null) { throw new ArgumentNullException("Enter the valid username."); }
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.NameIdentifier, user.UserId),
                new(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}

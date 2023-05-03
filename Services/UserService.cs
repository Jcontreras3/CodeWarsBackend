using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeWarsBackend.Models;
using CodeWarsBackend.Models.DTO;
using CodeWarsBackend.Services.Context;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace CodeWarsBackend.Services
{
    public class UserService : ControllerBase
    {
         private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

         public bool DoesUserExist(string? Username)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Username == Username) != null;
        }

         public bool AddUser(CreateAccountDTO UserToAdd)
        {
            bool result = false;

            if (!DoesUserExist(UserToAdd.Username))
            {
                UserModel newUser = new UserModel();

                var hashPassword = HashPassword(UserToAdd.Password);
                newUser.Id = UserToAdd.Id;
                newUser.Username = UserToAdd.Username;
                newUser.Salt = hashPassword.Salt;
                newUser.Hash = hashPassword.Hash;

                _context.Add(newUser);

                result = _context.SaveChanges() != 0;
            }
            return result;
        }

         public PasswordDTO HashPassword(string password)
        {
            PasswordDTO newHashedPassword = new PasswordDTO();
            byte[] SaltByte = new byte[64];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(SaltByte);

            var Salt = Convert.ToBase64String(SaltByte);

            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltByte, 10000);

            var Hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            newHashedPassword.Salt = Salt;
            newHashedPassword.Hash = Hash;

            return newHashedPassword;


        }

         public bool VerifyUserPassword(string? Password, string? storedHash, string? storedSalt)
        {
            // get our existing Sal and change it to base 64 string
            var SaltBytes = Convert.FromBase64String(storedSalt);

            // Making a password that the user input and using the stored salt
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Password, SaltBytes, 10000);
            // created the new hash
            var newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            return newHash == storedHash;
        }

        public IActionResult Login(LoginDTO User)
        {
           IActionResult Result = Unauthorized();
            if (DoesUserExist(User.Username))
            {
                UserModel foundUser = GetUserByUsername(User.Username);
                if (VerifyUserPassword(User.Password, foundUser.Hash, foundUser.Salt))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(
                       issuer: "http://localhost:5000",
                       audience: "http://localhost:5000",
                       claims: new List<Claim>(),
                       expires: DateTime.Now.AddMinutes(30),
                       signingCredentials: signinCredentials
                   );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    Result = Ok(new { Token = tokenString });
                }
            }
            return Result;
        }

        public IActionResult AdminLogin(AdminLoginDTO User)
        {
           IActionResult Result = Unauthorized();
           if(User.IsAdmin == true){
            if (DoesUserExist(User.Username))
            {
                UserModel foundUser = GetUserByUsername(User.Username);
                if (VerifyUserPassword(User.password, foundUser.Hash, foundUser.Salt))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(
                       issuer: "http://localhost:5000",
                       audience: "http://localhost:5000",
                       claims: new List<Claim>(),
                       expires: DateTime.Now.AddMinutes(30),
                       signingCredentials: signinCredentials
                   );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    Result = Ok(new { Token = tokenString });
                }
            }}
            return Result;
        }

        public UserModel GetUserByUsername(string? username){
            return _context.UserInfo.SingleOrDefault(user => user.Username == username);
        }

         public bool UpdateUser(UserModel userToUpdate)
        {
            // This one is sending over the whole object to be updated
            _context.Update<UserModel>(userToUpdate);
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<UserModel> GetAllUsers(){
            return _context.UserInfo;
        }

    }
}
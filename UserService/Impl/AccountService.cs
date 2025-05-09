using UserService.Helpers;
using UserService.Interfaces;
using UserService.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MeetingRoomApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace UserService.Impl
{
    public class AccountService : IAccountService
    {
        private readonly ITokenManager _tokenManager;
        private readonly dbContext _dbContext;
        private readonly Account _account;
        private readonly CredentialAttr _appSettings;
        public AccountService(dbContext context, ITokenManager tokenManager, IOptions<CredentialAttr> appSettings)
        {
            _tokenManager = tokenManager;
            _dbContext = context;
            _appSettings = appSettings.Value;
        }

        public async Task<ResLogin> Login(ReqLogin request)
        {
            var resLogin = new ResLogin();
            try
            {
                // Ambil user dari database berdasarkan username
                var getUser = await _dbContext.Account.FirstOrDefaultAsync(x => x.Username == request.Username);

                if (getUser == null)
                {
                    resLogin.STATUS = Constants.ResponseConstant.Invalid;
                    resLogin.MESSAGE = "User not found";
                    return resLogin;
                }

                // Verifikasi password
                var hasher = new PasswordHasher<Account>();
                var verifyResult = hasher.VerifyHashedPassword(getUser, getUser.Password, request.Password);

                if (verifyResult == PasswordVerificationResult.Failed)
                {
                    resLogin.STATUS = Constants.ResponseConstant.Invalid;
                    resLogin.MESSAGE = "Invalid password";
                    return resLogin;
                }

                if (!getUser.IsActive.HasValue || !getUser.IsActive.Value)
                {
                    resLogin.STATUS = Constants.ResponseConstant.Invalid;
                    resLogin.MESSAGE = Constants.ResponseConstant.InvalidLogin;
                    return resLogin;
                }

                // Ambil data role
                var getUserRole = await _dbContext.AccountRole.FirstOrDefaultAsync(x => x.AccountId == getUser.Id);
                var getUserRoleName = await _dbContext.TblMasterRole.FirstOrDefaultAsync(x => x.Id == getUserRole.RoleId);

                // Generate JWT
                var KeyJWT = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("USERID", getUser.UserId.ToString()),
                        new Claim("USERNAME", getUser.Username),
                        new Claim("ACCOUNTID", getUser.Id.ToString()),
                        new Claim("ROLEID", getUserRole.RoleId.ToString()),
                        new Claim("ROLENAME", getUserRoleName.RoleName.ToString()),
                        new Claim(ClaimTypes.Role, getUserRoleName.RoleName)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(double.Parse(_appSettings.Tokenlifetime)),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(KeyJWT),
                        SecurityAlgorithms.HmacSha256Signature),
                    Issuer = _appSettings.Issuer,
                    Audience = _appSettings.Audience
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                resLogin.jwt_token = tokenHandler.WriteToken(token);
                resLogin.STATUS = Constants.ResponseConstant.Success;
                resLogin.MESSAGE = Constants.ResponseConstant.LoginSuccess;
            }
            catch (Exception ex)
            {
                resLogin.STATUS = Constants.ResponseConstant.Invalid;
                resLogin.MESSAGE = ex.Message;
            }

            return resLogin;
        }
    }
}

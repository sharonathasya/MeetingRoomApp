using UserService.Helpers;
using UserService.Interfaces;
using UserService.ViewModels;
using MeetingRoomApp.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace UserService.Impl
{
    public class UserServices : IUserService
    {
        public readonly ITokenManager _tokenManager;
        public readonly dbContext _dbContext;

        public UserServices(dbContext context, ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
            _dbContext = context;
        }

        #region Register User
        public async Task<UserRes> Register(ReqAddUser request)

        {
            DateTime aDate = DateTime.Now;
            UserRes userRes = new();
            var hasher = new PasswordHasher<Account>();
            var hashedPass = hasher.HashPassword(null, request.Password);


            try
            {
                var currentUser = _tokenManager.GetPrincipal();
                var checkdata = _dbContext.Account.Where(x => x.Username == request.Username).FirstOrDefault();
                if (checkdata == null)
                {
                    User insertUser = new User
                    {
                        Email = request.Email,
                        Phone = request.Phone,
                        Address = request.Address,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Gender = request.Gender,
                        BirthDate = request.BirthDate,
                        CreatedTime = aDate
                    };
                    _dbContext.User.Add(insertUser);
                    _dbContext.SaveChanges();


                    var getUserId = _dbContext.User.Where(x => x.Email == request.Email).FirstOrDefault();
                    Account insertAcc = new Account
                    {
                        UserId = getUserId.UserId,
                        Username = request.Username,
                        Email = request.Email,
                        Password = hashedPass,
                        CreatedTime = aDate,
                        IsActive = true
                    };
                    _dbContext.Account.Add(insertAcc);
                    _dbContext.SaveChanges();

                    var getAccId = _dbContext.Account.Where(x => x.Username == request.Username).FirstOrDefault();
                    AccountRole accRole = new AccountRole
                    {
                        AccountId = (int)getAccId.Id,
                        RoleId = request.RoleId
                    };
                    _dbContext.AccountRole.Add(accRole);
                    _dbContext.SaveChanges();

                    userRes.STATUS = Constants.ResponseConstant.Success;
                    userRes.MESSAGE = Constants.ResponseConstant.SubmitSuccess;
                }
                else
                {
                    userRes.STATUS = Constants.ResponseConstant.Failed;
                    userRes.MESSAGE = Constants.ResponseConstant.UsernameExist;
                }

            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                userRes.STATUS = Constants.ResponseConstant.Failed;
                userRes.MESSAGE = errmsg;
            }

            return userRes;
        }
        #endregion

        #region Get User By Email
        public async Task<UserRes> GetUserByEmail(ReqIdUser request)
        {
            UserRes userRes = new();
            DataUser dataUser = new();
            try
            {
                var getData = _dbContext.Account.Where(x => x.Email == request.Email).FirstOrDefault();
                if (getData != null)
                {
                    dataUser = new()
                    {
                        Userid = getData.UserId,
                        Username = getData.Username,
                        Email = getData.Email,
                        CreatedTime = getData.CreatedTime
                    };

                    userRes.STATUS= Constants.ResponseConstant.Failed;
                    userRes.MESSAGE= Constants.ResponseConstant.EmailExist;
                    userRes.RESULT = dataUser;
                }
                else
                {
                    userRes.STATUS = Constants.ResponseConstant.NotFound;
                    userRes.MESSAGE = Constants.ResponseConstant.DataNotFound;
                }
            }
            catch (Exception ex) 
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                userRes.STATUS = Constants.ResponseConstant.Failed;
                userRes.MESSAGE = errmsg;
            }
            return userRes;
        }
        #endregion
    }
}

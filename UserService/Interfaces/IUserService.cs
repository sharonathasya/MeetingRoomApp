using UserService.ViewModels;

namespace UserService.Interfaces
{
    public interface IUserService
    {
        Task<UserRes> Register(ReqAddUser request);
        Task<UserRes> GetUserByEmail(ReqIdUser request);
    }
}

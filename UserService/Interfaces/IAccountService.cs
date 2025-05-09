using UserService.ViewModels;

namespace UserService.Interfaces
{
    public interface IAccountService
    {
        Task<ResLogin> Login(ReqLogin request);
    }
}

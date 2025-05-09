using UserService.Interfaces;
using UserService.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController(IAccountService accountService) : ControllerBase
    {
        public readonly IAccountService accountService = accountService;


        [HttpPost("Login")]
        public async Task<ResLogin> Login([FromBody] ReqLogin request)
        {
            return await accountService.Login(request);
        }
    }
}

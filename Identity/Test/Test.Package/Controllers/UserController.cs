using Identity.Client.Models;
using Identity.Client.Rules;
using Microsoft.AspNetCore.Mvc;

namespace Test.Package.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        readonly IAccountAction _account;

        public UserController(IAccountAction account)
        {
            _account = account;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            List<Guid> ids = new()
            {
                Guid.Parse("e0491339-35ab-4cad-8428-03e83dd980b5")
            };
            List<User> users = new();


            return Ok(users);
        }
    }
}
using Identity.Client.Models;
using Identity.Client.Rules;
using Microsoft.AspNetCore.Mvc;

namespace Test.Package.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        readonly IAccountRules _account;

        public UserController(IAccountRules account)
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
            await _account.GetUsersStreamAsync(ids, (user) =>
            {
                users.Add(user);
            });

            return Ok(users);
        }
    }
}
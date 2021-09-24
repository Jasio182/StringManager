using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public Task<IActionResult> AddUserAsync()
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public Task<IActionResult> ModifyUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}

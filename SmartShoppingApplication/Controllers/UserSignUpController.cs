using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using System.Linq;
using Mock;
using common.DTOs;
using Service;
using Service.Utils;
using Microsoft.AspNetCore.Authorization;
using common.Interfaces;
namespace SmartShoppingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserSignUpController : ControllerBase
    {
        private readonly IService<UserSignUpDto> _context;

        public UserSignUpController(IService<UserSignUpDto> context)
        {
            _context = context;
        }
        [HttpPost("signup")]
        public void SignUp([FromForm] UserSignUpDto value)
        {
            _context.AddItem(value);
        }
    }
}
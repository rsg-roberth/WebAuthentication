using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    
    [Route("")]
    [Authorize]
    public class GeneralController:ControllerBase
    {
        private IUserService _userService;
        private IPasswordService _passwordService;
        
        public GeneralController(IUserService userService,IPasswordService passwordService)
        {
            _userService = userService;
            _passwordService = passwordService;
        }
                
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody]AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);
            if(response == null) return Ok(new{authenticate = false});
            return Ok(response);            
        }

        [HttpGet("{password}")]
        public ActionResult ValidatePassword(string password)
        {
            var response = _passwordService.ValidatePassword(password);
            return Ok(response);
        }

        [HttpGet]
        public ActionResult CreatePassword()
        {
            var response = _passwordService.CreatePassword();
            return Ok(response);
        }
        
    }
}
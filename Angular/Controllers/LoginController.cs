using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiBankWebsiteA2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Angular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginDataAccessLayer _loginDataAccessLayer;

        public LoginController(LoginDataAccessLayer loginDataAccessLayer) {
            _loginDataAccessLayer = loginDataAccessLayer;
        }

        [HttpGet]
        [Route("Index")]
        public IEnumerable<Login> Get() 
        {
            return _loginDataAccessLayer.GetAllLogins();
        }

        [HttpGet]
        [Route("details/login/{id}")]
        public Login GetLogin(int id)
        {
            return _loginDataAccessLayer.getLoginById(id);
        }

        [HttpPut]
        [Route("Edit")]
        public int Edit([FromBody] Login login)
        {
            return _loginDataAccessLayer.UpdateLogin(login);
        }

        [HttpPut]
        [Route("LockOrUnlock")]
        public int LockOrUnlock([FromBody] int loginID) {
            var login = _loginDataAccessLayer.getLoginById(loginID);
            login.LockOrUnlock();
            return _loginDataAccessLayer.UpdateLogin(login);
        }
    }
}

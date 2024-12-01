using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Entities;
using Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        // GET: api/<UsersController>
        [HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return 
        //}

        //// GET api/<UsersController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<UsersController>
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> Login([FromQuery] string userName , [FromQuery] string password)
        {
            User user=await _userServices.LoginUser(userName, password);
            if (user != null) { 
                   return Ok(user);}
             return BadRequest();


        }

        [HttpPost]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            User userRegister =await _userServices.RegisterUser(user);
            if (userRegister != null)
            {
                if(userRegister.FirstName== "Weak password")
                {
                    return NoContent();
                }
                return Ok(userRegister);
            }  
            return BadRequest();
        }

        [HttpPost]
        [Route("password")]
        public int CheckPassword([FromBody] String password)
        {
            return _userServices.CheckPassword(password);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, [FromBody] User userToUpdate)
        {
            User userUpdate =await _userServices.UpdateUser(id,userToUpdate);
            if (userUpdate != null)
            {
                if (userUpdate.FirstName == "Weak password")
                {
                    return NoContent();
                }
                return Ok(userUpdate);
            }
            return BadRequest();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

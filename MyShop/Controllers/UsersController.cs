using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Entities;
using Services;
using DTO;
using AutoMapper;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userServices;
        IMapper _mapper;
        public UsersController(IUserService userServices, IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<GetUserDTO> Get(int id)
        {
            User user = await _userServices.GetById(id);
            return _mapper.Map<User, GetUserDTO>(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<GetUserDTO>> Login([FromQuery] string userName , [FromQuery] string password)
        {
            User user=await _userServices.LoginUser(userName, password);
            if (user != null)
            { 
                   return Ok(_mapper.Map<User, GetUserDTO>(user));
            }
             return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register([FromBody] RegisterUserDTO registerUserDTO)
        {
            User user = _mapper.Map<RegisterUserDTO, User>(registerUserDTO);
            User userRegister =await _userServices.RegisterUser(user);
            if (userRegister != null)
            {
                if(userRegister.FirstName== "Weak password")
                {
                    return NoContent();
                }
                return Ok(_mapper.Map<User, GetUserDTO>(userRegister));
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
        public async Task<ActionResult<User>> Put(int id, [FromBody] RegisterUserDTO userToUpdateDTO)
        {
            User user = _mapper.Map<RegisterUserDTO, User>(userToUpdateDTO);
            User userUpdate =await _userServices.UpdateUser(id, user);
            if (userUpdate != null)
            {
                if (userUpdate.FirstName == "Weak password")
                {
                    return NoContent();
                }
                return Ok(_mapper.Map<User, GetUserDTO>(userUpdate));
            }
            return BadRequest();
        }

        //// DELETE api/<UsersController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

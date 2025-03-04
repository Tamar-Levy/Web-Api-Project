using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Entities;
using Services;
using DTO;
using AutoMapper;
using Microsoft.Extensions.Logging;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userServices;
        IMapper _mapper;
        ILogger<UsersController> _logger;

        public UsersController(IUserService userServices, IMapper mapper, ILogger<UsersController> logger)
        {
            _userServices = userServices;
            _mapper = mapper;
            _logger = logger;
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
                _logger.LogInformation($"User name: {user.UserName} Name: {user.FirstName} {user.LastName}");
                return Ok(_mapper.Map<User, GetUserDTO>(user));
            }
             return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register([FromBody] RegisterUserDTO registerUserDTO)
        {
            try
            {
                User user = _mapper.Map<RegisterUserDTO, User>(registerUserDTO);
                User userRegister = await _userServices.RegisterUser(user);
                if (userRegister != null)
                {
                    return Ok(_mapper.Map<User, GetUserDTO>(userRegister));
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return Conflict(new { message = "Weak password" });
            }
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
            try { 
            User user = _mapper.Map<RegisterUserDTO, User>(userToUpdateDTO);
            User userUpdate =await _userServices.UpdateUser(id, user);
            if (userUpdate != null)
            {
                return Ok(_mapper.Map<User, GetUserDTO>(userUpdate));
            }
            return BadRequest();
            }
             catch (Exception ex)
            {
                return Conflict(new { message = "Weak password" });
            }
        }
    }
}

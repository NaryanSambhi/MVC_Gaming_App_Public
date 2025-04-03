using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Z_Team_Group_Project.Data;
using Z_Team_Group_Project.Models;
using Z_Team_Group_Project.Services;

namespace Z_Team_Group_Project.Controllers
{

    [Route("api/")]
    [ApiController]
    public class RestfulAPIController : ControllerBase
    {
        private readonly IZTeamAPIService _zTeamAPIService;

        public RestfulAPIController(IZTeamAPIService zTeamAPIService)
        {
            _zTeamAPIService = zTeamAPIService;
        }


        /// <summary>
        /// Get a list of users from the db 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AllUsers")]
        public IEnumerable<User> GetUsers()
        {
            return _zTeamAPIService.Users();
        }

        /// <summary>
        /// Get a user by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public User GetUserById(int id)
        {
            User user = new User();
            user =  _zTeamAPIService.GetByid(id);

            return user;
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("CreateUser")]
        public IActionResult Create([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User object is null");
            }

            var createdUser = _zTeamAPIService.CreateUser(user);

            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPost]
        [Route("EditUser/{id}")]
        public IActionResult Edit(int id, [FromBody] User user)
        {
            if (user == null || user.Id != id)
            {
                return BadRequest("Invalid user data.");
            }

            try
            {
                var updatedUser =  _zTeamAPIService.UpdateUser(user);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var result =  _zTeamAPIService.DeleteUser(id);

            if (!result)
            {
                return NotFound("User not found."); // 404 Not Found
            }

            return NoContent(); // 204 No Content (successful deletion)
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticationViewModel authenticationViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _zTeamAPIService.AuthenticateUser(authenticationViewModel.Username, authenticationViewModel.Password);

                if (user != null)
                {
                    return Ok(new { Success = true, Message = "Logged In!" });
                }
                else
                {
                    return Unauthorized(new { Success = false, Message = "Invalid credentials." });
                }
            }
            else
            {
                return BadRequest(new { Success = false, Message = "Invalid input." });
            }
        }

        [HttpGet("details/{id}")]
        public IActionResult UserDetails(int id)
        {
            var result = _zTeamAPIService.Details(id);

            return Ok(result);

        }




    }
}

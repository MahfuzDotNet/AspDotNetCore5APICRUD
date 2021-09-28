using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetApi.Classes;
using PetApi.Models;
using PetApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController(UserService userService)
        {
            UserService = userService;
        }

        public UserService UserService { get; }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] QueryParameters queryParameters)
        {
            return Ok(await UserService.GetAllUsers(queryParameters).ToArrayAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {           
            var user = await Task.Run(() => UserService.GetUser(id));

            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {
            await Task.Run(() =>UserService.AddUser(user));

            return CreatedAtAction(
                "GetAllUsers",
                new { id = user.Id },
                user
            );
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            var updateUser = new User();

            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                updateUser = await Task.Run(()=> UserService.ModifyUser(id, user));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (updateUser == null)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await Task.Run(()=> UserService.DeleteUser(id));

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> DeleteMultiple([FromQuery] int[] ids)
        {
            var users = await Task.Run(() => UserService.DeleteMultipleUser(ids));

            if (users == null) return NotFound();

            return Ok(users);
        }
    }
}

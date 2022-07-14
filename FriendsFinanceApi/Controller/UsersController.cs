using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FriendsFinanceApi.Repository.Models;
using FriendsFinanceApi.Repository;

namespace FriendsFinanceApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        private async Task<int> getCurrentUserId()
        {
            return 1;
        }

        public UsersController(DataContext context)
        {
            _context = context;
        }

        

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.UserResponse>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var userId = await getCurrentUserId();
            var activities = _context.Activitys.Where(x => x.OwnerId == userId).ToList();
            
            Dictionary<int, int> sum = new Dictionary<int, int>();
            foreach(var activity in activities)
            {
                var activitiesMembers = _context.ActivityMembers.Where(x => x.Activity.OwnerId == userId && x.ActivityId == activity.Id).ToList();
                var count = activitiesMembers.Count() + 1;
                foreach (var member in activitiesMembers)
                {
                    sum.TryGetValue(member.UserId, out int result);
                    result += activity.Sum / count;
                    sum[member.UserId] = result;
                }
            }
            foreach(var user in sum)
            {
                Console.WriteLine($"{user.Key} - {user.Value}");
            }
            var users = await _context.Users.Where(x => sum.Select(x => x.Key).Contains(x.Id)).ToListAsync();
            return users.Select(x => new Models.UserResponse()
            {
                Id = x.Id,
                Name = x.Name,
                DebtSum = sum[x.Id]
            }).ToList();
        }


        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<User>>> Search(string name)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.Where(x => x.Name.ToLower().Contains(name.ToLower()) && x.Id != 1).ToListAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationContext.Users'  is null.");
            }
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FriendsFinanceApi.Repository;
using FriendsFinanceApi.Repository.Models;

namespace FriendsFinanceApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly DataContext _context;

        private async Task<int> getCurrentUserId()
        {
            return 1;
        }

        public FriendsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Friends
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.UserResponse>>> GetFriends()
        {


            if (_context.Users == null)
            {
                return NotFound();
            }
            var userId = await getCurrentUserId();
            var activities = _context.Activitys.Where(x => x.OwnerId == userId).ToList();

            Dictionary<int, int> sum = new Dictionary<int, int>();
            foreach (var activity in activities)
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
            foreach (var user in sum)
            {
                Console.WriteLine($"{user.Key} - {user.Value}");
            }
            var users = await _context.Users.Where(x => sum.Select(x => x.Key).Contains(x.Id)).ToListAsync();
            var myFriends = await _context.Friends.Where(x => x.RequestedById == 1).Select(x => x.Friend).ToListAsync();
            var response = new List<Models.UserResponse>();
            myFriends.ForEach(item => {
                sum.TryGetValue(item.Id, out var res);
                response.Add(new Models.UserResponse()
                {
                    Id = item.Id,
                    Name = item.Name,
                    DebtSum = res
                });
            });
            return response;
        }

        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<Models.UserResponse>>> Search(string name)
        {
            if (_context.Friends == null)
            {
                return NotFound();
            }
            return await _context.Friends.Where(x => x.RequestedById == 1 && x.Friend.Name.ToLower().Contains(name.ToLower())).Select(x => new Models.UserResponse()
            {
                Id = x.Friend.Id,
                Name = x.Friend.Name
            }).ToListAsync();
        }

        // GET: api/Friends/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FriendModel>> GetFriends(int id)
        {
            if (_context.Friends == null)
            {
                return NotFound();
            }
            var friends = await _context.Friends.FindAsync(id);

            if (friends == null)
            {
                return NotFound();
            }

            return friends;
        }

        // POST: api/Friends
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FriendModel>> PostFriends(int friendId)
        {
            if (_context.Friends == null)
            {
                return Problem("Entity set 'DataContext.Friends'  is null.");
            }
            var friend = new FriendModel()
            {
                FriendId = friendId,
                RequestedById = 1
            };
            _context.Friends.Add(friend);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFriends", new { id = friend.Id }, friend);
        }

        // DELETE: api/Friends/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriends(int id)
        {
            if (_context.Friends == null)
            {
                return NotFound();
            }
            var friends = await _context.Friends.FindAsync(id);
            if (friends == null)
            {
                return NotFound();
            }

            _context.Friends.Remove(friends);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FriendsExists(int id)
        {
            return (_context.Friends?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

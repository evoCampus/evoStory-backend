using EvoStory.Database.Data;
using EvoStory.Database.Exceptions;
using EvoStory.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EvoStory.Database.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUser(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> DeleteUser(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return user;
        }

        public async Task<User> Login(string username, string password)
        {

            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }
       
    }
}

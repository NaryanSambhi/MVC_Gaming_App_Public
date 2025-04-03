using Z_Team_Group_Project.Models;
using Z_Team_Group_Project.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Z_Team_Group_Project.Services
{
    public class ZTeamAPIService : IZTeamAPIService
    {
        Z_Team_Group_ProjectContext _context;

        public ZTeamAPIService(Z_Team_Group_ProjectContext context)
        {
            _context = context;
        }

        public User GetByid(int id)
        {
            User user = new User();
            user = _context.User
                .Include(a => a.Account)
                .FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new Exception(
                    "No user found.");
            }
            return user;
        }

        public IEnumerable<User> Users()
        {
            return _context.User
                .Include(a => a.Account);
        }

        public User CreateUser(User user)
        {
            var account = new Account { Amount = 0 };
            _context.Accounts.Add(account);
             _context.SaveChanges();

            user.AccountId = account.AccountId;
            _context.User.Add(user);
             _context.SaveChanges();

            return user;
        }
        public bool DeleteUser(int id)
        {
            var user =  _context.User
                .Include(a => a.Account)
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return false; // User not found
            }

            _context.User.Remove(user);
             _context.SaveChanges();

            return true; // User deleted successfully
        }

        public User Details(int id)
        {
            var user = _context.User
                .Include(a => a.Account)
                .FirstOrDefault(u => u.Id == id);

            return user;
        }

        public User UpdateUser(User editedUser)
        {
            var user = _context.User
                    .Include(a => a.Account)
                    .FirstOrDefault(u => u.Id == editedUser.Id);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            user.FirstName = editedUser.FirstName;
            user.LastName = editedUser.LastName;
            user.Email = editedUser.Email;

             _context.SaveChanges();

            return user;

        }

        public User AuthenticateUser(string email, string password)
        {
            var user =  _context.User
                .FirstOrDefault(u => u.Email == email);

            if(user != null)
            {
                if (user.Password == password)
                {
                    _context.SaveChanges();
                    return user;
                }
            }

            return null;
        }
    }


    



}

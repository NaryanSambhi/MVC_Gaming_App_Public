using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Z_Team_Group_Project.Data;

namespace Z_Team_Group_Project.Models
{
    public class AuthenticationViewModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Result { get; set; }

        public void setUsername(string? username)
        {
            Username = username;
        }
        public void setPassword(string? password)
        {
            Password = password;
        }

        // Instance method
        public async Task<string> GetResult(Z_Team_Group_ProjectContext context)
        {
            string summary;

            // Query the database to find the user by username
            var user = await context.User
                .FirstOrDefaultAsync(u => u.Email == Username);

            if (user != null && user.Password == Password) // Check if password matches
            {
                summary = "Logged In!";
            }

            else
            {
                setUsername("");
                setPassword("");
                summary = "Unable to login";
            }

            return summary;
        }
    }

}

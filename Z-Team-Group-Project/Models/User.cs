using System.ComponentModel.DataAnnotations;

namespace Z_Team_Group_Project.Models
{
    public class User
    {

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        //reference to the account for this user
        public int? AccountId { get; set; }

        public Account? Account { get; set; }
    }
}

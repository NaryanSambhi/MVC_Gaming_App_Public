using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Z_Team_Group_Project.Models
{
    public class SignedIn
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string SessionToken { get; set; }

    }
}

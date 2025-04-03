using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Z_Team_Group_Project.Models;

namespace Z_Team_Group_Project.Data
{
    public class Z_Team_Group_ProjectContext : DbContext
    {

        public Z_Team_Group_ProjectContext (DbContextOptions<Z_Team_Group_ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<Z_Team_Group_Project.Models.User> User { get; set; } = default!;
        public DbSet<Account> Accounts { get; set; }

       

    }
}

using Z_Team_Group_Project.Models;
namespace Z_Team_Group_Project.Services
{
    public interface IZTeamAPIService
    {
        IEnumerable<User> Users();
        User GetByid(int id);

        User CreateUser(User user);

        bool DeleteUser(int id);

        User AuthenticateUser(string email, string password);

        User Details(int id);

        User UpdateUser(User user);
    }
}

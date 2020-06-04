using System.Linq;
using Order_Management.Domain;
using Order_Management.Identity.Model;

namespace Order_Management.Identity.Repository
{
    public class UserRepository: IUserRepository
    {
        private ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int AddUser(User user)
        {
            _dbContext.Users.Add(user);
            return _dbContext.SaveChanges();
        }

        public User GetUser(string email)
        {
            return (from User in _dbContext.Users where User.Email == email select User).FirstOrDefault<User>();
        }
    }
}

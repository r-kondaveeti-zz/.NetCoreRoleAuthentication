using System;
using Order_Management.Identity.Model;

namespace Order_Management.Identity.Repository
{
    public interface IUserRepository
    {
        User GetUser(string email);
        int AddUser(User user);
    }
}

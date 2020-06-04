using System;
using System.Threading.Tasks;

namespace Order_Management.Identity.Service
{
    public interface IUserService
    {
        int RegisterUser(string name, string email, string password, string role, string tenantId);
    }
}

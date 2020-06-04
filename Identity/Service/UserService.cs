using System;
using System.Threading.Tasks;
using Order_Management.Identity.Helper;
using Order_Management.Identity.Model;
using Order_Management.Identity.Repository;

namespace Order_Management.Identity.Service
{
    public class UserService: IUserService
    {
        IUserRepository _userRepository;
        IEncrypter _encrypter;

        public UserService(IUserRepository userRepository, IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
        }

        public int RegisterUser(string name, string email, string password, string role, string tenantId)
        {
            var user = _userRepository.GetUser(email);

            if (user != null)
            {
                throw new Exception("email_is_already_in_use");
            }

            user = new User(name, email, role, tenantId);

            user.SetPassword(password, _encrypter);
            return _userRepository.AddUser(user);
        }
    }
}

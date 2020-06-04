using System;
using Order_Management.Identity.Helper;

namespace Order_Management.Identity.Model
{
    public class User
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string Role { get; protected set; }
        public string TenantId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected User()
        {
        }

        public User(string name, string email, string role, string tenantId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("empty_user_name");
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("empty_user_email");
            }

            Id = Guid.NewGuid();
            Name = name;
            Email = email.ToLowerInvariant();
            Role = role.ToLowerInvariant();
            TenantId = tenantId.ToLowerInvariant();
            CreatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, IEncrypter encrypter)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("empty_password");
            }

            Salt = encrypter.GetSalt();
            Password = encrypter.GetHash(password, Salt);
        }

        public bool ValidatePassword(string password, IEncrypter encrypter)
            => Password.Equals(encrypter.GetHash(password, Salt));
    }
}

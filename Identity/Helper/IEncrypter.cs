using System;
namespace Order_Management.Identity.Helper
{
    public interface IEncrypter
    {
        string GetSalt();
        string GetHash(string value, string salt);
    }
}

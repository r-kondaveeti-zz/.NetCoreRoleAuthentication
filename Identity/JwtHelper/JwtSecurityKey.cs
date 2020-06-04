using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Order_Management.Identity.JwtHelper
{
    public class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}

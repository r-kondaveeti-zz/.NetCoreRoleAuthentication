//using System;
//using Microsoft.Extensions.Configuration;
//using Order_Management.Identity.Model;

//namespace Order_Management.Identity.JwtHelper
//{
//    public class JwtExtensions
//    {
//        public static void GenerateToken(this User user, IConfiguration configuration)
//        {
//            try
//            {
//                var token = new JwtTokenBuilder()
//                                .AddSecurityKey(JwtSecurityKey.Create(configuration.GetValue<string>("JwtSecretKey")))
//                                .AddIssuer(configuration.GetValue<string>("JwtIssuer"))
//                                .AddAudience(configuration.GetValue<string>("JwtAudience"))
//                                .AddExpiry(30)
//                                .AddClaim("Id", user.Id.ToString())
//                                .AddRole("User")
//                                .Build();

//                user.Token = token.Value;
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }
//    }
//}

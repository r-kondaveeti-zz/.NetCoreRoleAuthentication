using System;
using Order_Management.Identity.JwtHelper;

namespace Order_Management.ResponseModel
{
    public class TokenApiResponse
    {
        public string Message { get; set; }

        public JwtToken Token { get; set; }
    }
}

using System;
namespace Order_Management.ResponseModel
{
    public class ApiResponse
    {
        public ApiResponse()
        {
        }

        public string Message { get; set; }

        public Object Data { get; set; }
    }
}

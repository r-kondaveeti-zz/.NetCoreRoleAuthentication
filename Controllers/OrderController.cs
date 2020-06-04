using System;
using Microsoft.AspNetCore.Mvc;
using Order_Management.Domain.Model;
using Order_Management.Domain;
using Order_Management.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;

namespace Order_Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult PutOrder([FromBody] OrderModel orderModel)
        {
            Order order = new Order { Name = orderModel.Name, Status = orderModel.Status, TenantId = ExtractTenantId() };
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return Ok(new ApiResponse { Message = "order inserted", Data = order });
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public IActionResult GetOrder()
        {
            return Ok(new ApiResponse { Message = "all orders", Data = _dbContext.GetAllOrders(ExtractTenantId()) });
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteOrder()
        {
            Guid id = new Guid(HttpContext.Request.Query["id"].ToString());
            string tenantId = ExtractTenantId();
            var order = _dbContext.GetOrder(id, tenantId);

            if (order == null)
            {
                return Ok(new ApiResponse { Message = "order not found", Data = null });
            }

            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();
            Console.WriteLine(id);
            
            return Ok(new ApiResponse { Message = "order deleted", Data = order});
        }

        private string ExtractTenantId()
        {
            string header = HttpContext.Request.Headers["Authorization"].ToString();
            header = header.Replace("Bearer ", String.Empty);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(header);
            var tokenS = handler.ReadToken(header) as JwtSecurityToken;

            var tenantId = tokenS.Claims.First(claim => claim.Type == "TenantId").Value;

            return tenantId;
        }

    }

    public class OrderModel
    {
        public string Name { get; set; }
        public string Status { get; set; }
    }
}

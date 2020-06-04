using Microsoft.EntityFrameworkCore;
using System.Linq;
using Order_Management.Domain.Model;
using Order_Management.Identity.Model;
using System;

namespace Order_Management.Domain
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }

        public Order GetOrder(Guid id, string tenantId)
        { 
            return (from Order in Orders where Order.Id == id & Order.TenantId == tenantId select Order).FirstOrDefault<Order>();
        }

        public IQueryable<Order> GetAllOrders(string tenantId)
        {
            return (from Order in Orders where Order.TenantId == tenantId select Order);
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order_Management.Domain.Model
{
    public class Order
    {
        private string _Name;
        private string _Status;
        private string _TenantId;

        public Order()
        {
        }

        [Key]
        public Guid Id { get; set; }

        public string Name { get { return _Name; } set { _Name = value.ToLowerInvariant(); } }

        public string Status { get { return _Status; } set { _Status = value.ToLowerInvariant(); } }

        public string TenantId { get { return _TenantId; } set { _TenantId = value.ToLowerInvariant(); } }

    }
}

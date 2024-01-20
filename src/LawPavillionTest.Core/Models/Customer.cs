using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawPavillionTest.Core.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = null!;
        public string CustomerEmail { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public short? Gender { get; set; }
        public string? AuthType { get; set; }
    }

}

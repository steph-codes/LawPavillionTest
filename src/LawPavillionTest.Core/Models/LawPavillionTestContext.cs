using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawPavillionTest.Core.Models
{
    public partial class LawPavillionTestContext : DbContext
    {
        public LawPavillionTestContext()
        {
        }

        public LawPavillionTestContext(DbContextOptions<LawPavillionTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; } = null!;
    }
}

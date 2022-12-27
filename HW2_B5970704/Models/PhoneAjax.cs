using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HW2_B5970704.Models
{
    public partial class PhoneAjax : DbContext
    {
        public PhoneAjax()
            : base("name=PhoneAjax")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

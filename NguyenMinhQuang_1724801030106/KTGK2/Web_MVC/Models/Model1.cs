namespace Web_MVC.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<phone> phones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<phone>()
                .Property(e => e.price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<phone>()
                .Property(e => e.gerenal_note)
                .IsFixedLength();
        }
    }
}

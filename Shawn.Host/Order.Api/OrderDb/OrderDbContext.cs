using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Order.Api.Domain;
using Order.Api.OrderDb.EntityframeworkConfig;

namespace Order.Api.OrderDb
{
    public class OrderDbContext: DbContext
    {
        public virtual DbSet<MainOrder> MainOrder { get; set; }
        public virtual DbSet<Buyer> Buyer { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEnitityConfig());
            modelBuilder.ApplyConfiguration(new OrderItemEnitityConfig());
            modelBuilder.ApplyConfiguration(new BuyerEnitityConfig()); 
            modelBuilder.ApplyConfiguration(new PaymentMethodEnitityConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}

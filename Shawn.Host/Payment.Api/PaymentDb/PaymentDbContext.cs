using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payment.Api.Domain;
using Payment.Api.PaymentDb.EntityConfig;

namespace Payment.Api.PaymentDb
{
    public class PaymentDbContext: DbContext
    {
        public virtual DbSet<PayedInfo> PayedInfo { get; set; }
        public virtual DbSet<RefundInfo> RefundInfo { get; set; }
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PayedInfoEnitityFrameConfig());
            modelBuilder.ApplyConfiguration(new RefundInfoEnitityConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}

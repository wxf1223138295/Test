using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.Api.Domain;

namespace Payment.Api.PaymentDb.EntityConfig
{

    public class PayedInfoEnitityFrameConfig : IEntityTypeConfiguration<PayedInfo>
    {
        public void Configure(EntityTypeBuilder<PayedInfo> builder)
        {
            builder.ToTable("PayedInfo");
            builder.HasKey(p => p.PayedId);
        }
    }
    public class RefundInfoEnitityConfig : IEntityTypeConfiguration<RefundInfo>
    {
        public void Configure(EntityTypeBuilder<RefundInfo> builder)
        {
            builder.ToTable("RefundInfo");
            builder.HasKey(p => p.RefundId);
        }
    }

}

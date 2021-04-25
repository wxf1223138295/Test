using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Api.Domain;

namespace Order.Api.OrderDb.EntityframeworkConfig
{
    public class OrderEnitityConfig : IEntityTypeConfiguration<MainOrder>
    {
        public void Configure(EntityTypeBuilder<MainOrder> builder)
        {
            builder.ToTable("MainOrder");
            builder.HasKey(p => p.OrderId);

            var navigation = builder.Metadata.FindNavigation(nameof(MainOrder.Items));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Property);



            builder.OwnsOne(o => o.Address, a =>
                {
                    a.WithOwner();
                });


        }
    }
    public class OrderItemEnitityConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder) 
        {
            builder.ToTable("OrderItem");
            builder.HasKey(p => p.ItemId);
        }
    }
    public class PaymentMethodEnitityConfig : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable("PaymentMethod");
            builder.HasKey(p => p.id);
        }
    }
    public class BuyerEnitityConfig : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
            builder.ToTable("Buyer");
            builder.HasKey(p => p.BuyId);

            builder.HasMany(b => b.PaymentMethods)
                .WithOne()
                .HasForeignKey("BuyerId")
                .OnDelete(DeleteBehavior.Cascade);

            var navigation = builder.Metadata.FindNavigation(nameof(Buyer.PaymentMethods));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}

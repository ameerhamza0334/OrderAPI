using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Data.Configurations
{
	public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.HasKey(x => x.ID);
			builder.Property(x => x.Price).IsRequired();
			builder.Property(x => x.Quantity).IsRequired();
		}
	}
}

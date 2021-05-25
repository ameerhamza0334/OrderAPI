using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Data.Configurations
{
	public class OrderItemConfig : IEntityTypeConfiguration<CSReceiptItem>
	{
		public void Configure(EntityTypeBuilder<CSReceiptItem> builder)
		{
			builder.HasKey(x => x.ID);
			builder.Property(x => x.Price).IsRequired();
			builder.Property(x => x.Quantity).IsRequired();
		}
	}
}

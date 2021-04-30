using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Data.Models;

namespace Orders.Data.Configurations
{
	public class OrderConfig : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasKey(x => x.ID);
			builder
				.Property(x => x.Address)
				.IsRequired()
				.HasMaxLength(100);
			builder.Property(x => x.IpAddress).HasMaxLength(20);
			builder.Property(x => x.MerchantID).IsRequired().HasMaxLength(20);
			builder.Property(x => x.OrderNo).IsRequired().HasMaxLength(20);
			builder.Property(x => x.CompanyName).HasMaxLength(50).IsRequired();
			builder.Property(x => x.PhoneNumber).HasMaxLength(20).IsRequired();
		}
	}
}

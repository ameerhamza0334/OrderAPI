using Microsoft.EntityFrameworkCore;
using Orders.Data.Configurations;
using Orders.Data.Models;

namespace Orders.Data
{
	public partial class OrderDbContext : DbContext
	{
		public OrderDbContext()
		{

		}

		public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
		{

		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
#if DEBUG
				optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=root;Database=OrdersDB;");
#else
				optionsBuilder.UseNpgsql("Server=coorderdb.postgres.database.azure.com;Database=ordersdb;Port=5432;User Id=copostgres@coorderdb;Password=root12!@;Ssl Mode=Require;");
#endif
			}
		}

		public DbSet<CSReceipt> Orders { get; set; }
		public DbSet<CSReceiptItem> OrderItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			new OrderConfig().Configure(modelBuilder.Entity<CSReceipt>());
			new OrderItemConfig().Configure(modelBuilder.Entity<CSReceiptItem>());
		}
	}
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orders.Data;
using Orders.Service.IServices;
using Orders.Service.Services;

namespace OrdersAPI
{
	public class Startup
	{
		readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			
			services.Add(new ServiceDescriptor(typeof(IOrdersService), new OrdersService()));
			services.AddCors(c =>
			{
				c.AddPolicy(MyAllowSpecificOrigins, p =>
				{
					p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(origin => true);
				});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			
			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			

			app.UseCors();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/echo",
				context => context.Response.WriteAsync("echo"))
				.RequireCors(MyAllowSpecificOrigins);
				endpoints.MapControllers().RequireCors(MyAllowSpecificOrigins);
			});


		}
	}
}

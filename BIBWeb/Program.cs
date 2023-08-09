using BIBData.Models;
using BIBData.Repositories;
using BIBServices;
using Microsoft.EntityFrameworkCore;

namespace BIBWeb
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// ConnectionString inlezen
			builder.Services.AddDbContext<BIBDbContext>(
				options => {
					options.UseSqlServer(
						builder.Configuration.GetConnectionString("BIBConnection"),
						x => x.MigrationsAssembly("BIBData")
						);
				});

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			// LenerService - SQLLenerRepo Injecteren
			builder.Services.AddTransient<LenerService>();
			builder.Services.AddTransient<ILenerRepository, SQLLenerRepository>();

			// UitleenobjectService - SQLUitleenobjectRepo Injecteren
			builder.Services.AddTransient<UitleenobjectService>();
			builder.Services.AddTransient<IUitleenobjectRepository, SQLUitleenobjectRepository>();

			// UitleeningService - SQLUitleningRepo Injecteren
			builder.Services.AddTransient<UitleeningService>();
			builder.Services.AddTransient<IUitleningRepository, SQLUitleningRepository>();

			// ReserveringService - SQLReserveringenRepo Injexteren
			builder.Services.AddTransient<ReserveringService>();
			builder.Services.AddTransient<IReserveringRepository, SQLReserveringenRepository>();


			// --------------------------------------------------
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment()) {
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
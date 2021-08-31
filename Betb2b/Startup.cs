using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using B2B.Domain.Dto;
using B2B.Repository;
using B2B.Services;
using B2B.Services.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Betb2b
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers()
				.AddXmlSerializerFormatters();
			services.AddSingleton<IUserCacheService, UserCacheService>();
			services.AddScoped<IUserService, UserService>();
			services.AddTransient<IB2BRepository, B2BRepository>();

			string connectionString = Configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<B2BContext>(options => options
								.UseSqlServer(connectionString));
			services.AddTransient<B2BContext>();

			services.AddHostedService<ReloadService>();

			services
				.AddAuthentication(o =>
				{
					o.DefaultScheme = "BasicAuthentication";
				})
				.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", options => { });
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseMiddleware(typeof(ExceptionMiddleware));

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}

//#define TEST

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using WebbiSkools.QuizManager.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebbiSkools.QuizManager.DAL.Context;
using WebbiSkools.QuizManager.DAL.Entities.Identity;
using WebbiSkools.QuizManager.BRL.Services.Abstractions;
using WebbiSkools.QuizManager.BRL.Services.Implementations;
using WebbiSkools.QuizManager.BRL.DataAccessMediators.Abstractions;
using WebbiSkools.QuizManager.BRL.ViewModels.Quiz;
using WebbiSkools.QuizManager.BRL.DataAccessMediators.Implementations;
using WebbiSkools.QuizManager.DAL.Operations.Abstractions;
using WebbiSkools.QuizManager.DAL.Entities.QuizManager;
using WebbiSkools.QuizManager.DAL.Operations.Implementations;
using AutoMapper;
using WebbiSkools.QuizManager.BRL.AutomapperProfiles;
using FluentValidation.AspNetCore;
using WebbiSkools.QuizManager.BRL.Validators.QuizManager;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace WebbiSkools.QuizManager
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>

#if TEST
				options.UseSqlServer(Configuration.GetConnectionString("TestConnection"),
#elif DEBUG
				options.UseSqlServer(Configuration.GetConnectionString("LocalConnection"),
#else
				options.UseSqlServer(Configuration.GetConnectionString("ProductionConnection"),
#endif
			sqlServerOptionsAction: sqlOptions =>
			{
				sqlOptions.MigrationsAssembly("WebbiSkools.QuizManager.DAL");
				sqlOptions.EnableRetryOnFailure(
				maxRetryCount: 10,
				maxRetryDelay: TimeSpan.FromSeconds(30),
				errorNumbersToAdd: null);
			}));

			services.AddIdentity<QuizManagerUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>();
			services.AddRazorPages().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining(typeof(QuizValidator)));

			services.ConfigureApplicationCookie(options =>
			{
				options.AccessDeniedPath = "/Identity/Account/AccessDenied";
				options.Cookie.Name = "WebbiSkoolsAuthenticationCookie";
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
				options.LoginPath = "/Identity/Account/Login";
				options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
				options.SlidingExpiration = true;
			});

			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});


			services.AddScoped<IQuizManagementProvider, QuizManagementService>();

			services.AddScoped<IDataAccessMediator<QuizViewModel>, QuizDataAccessMediator>();
			services.AddScoped<IDataAccessMediator<QuestionViewModel>, QuestionDataAccessMediator>();
			services.AddScoped<IDataAccessMediator<AnswerViewModel>, AnswerDataAccessMediator>();

			services.AddScoped<IDbOps<EAnswer>, OAnswer>();
			services.AddScoped<IDbOps<EQuestion>, OQuestion>();
			services.AddScoped<IDbOps<EQuiz>, OQuiz>();

			services.AddAutoMapper(config => 
				config.ForAllMaps((tm, me) => me.ForAllMembers(option => 
					option.Condition((source, destination, sourceMember) => sourceMember != null))),
				typeof(AnswerProfile));

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseStatusCodePagesWithRedirects("/Error");

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();
			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}
	}
}

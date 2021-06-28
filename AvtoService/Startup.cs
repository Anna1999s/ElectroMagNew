using DomainModel.Framework.Models;
using Interfaces.Content;
using Interfaces.Initializer;
using Interfaces.Localization;
using Interfaces.Localization.Common;
using Interfaces.Navigation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Services;
using Services.Content;
using Services.Initializer;
using Services.Localization;
using Services.Localization.Common;
using Services.Navigation;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Microsoft.EntityFrameworkCore.Proxies;
using System;

namespace WebSite
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
                options.UseLazyLoadingProxies().UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<IDbInitializer, DbInitializer>();
            services.AddTransient<IPageService, PageService>();
            services.AddTransient<IMenuItemService, MenuItemService>();
            services.AddTransient<IPhotoService, PhotoService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<INewService, NewService>();
            services.AddTransient<IBasketService, BasketService>();
            services.AddTransient<IBasketItemService, BasketItemService>();

            services.AddMemoryCache();

            services.Configure<GoogleTranslateConfig>(Configuration.GetSection("GoogleTranslateConfig"));

            #region localizationServices

            services.AddScoped<ICultureService, CultureService>();
            services.AddScoped<ILocalizationSetService, LocalizationSetService>();
            services.AddScoped<ILocalizationService, LocalizationService>();
            services.AddScoped<ILocalizedPageService, LocalizedPageRepository>();

            #endregion

            #region snippet1

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            #endregion

            services.Configure<RequestLocalizationOptions>(
                opts =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                        new CultureInfo("ru"),
                        new CultureInfo("ko")
                    };

                    opts.DefaultRequestCulture = new RequestCulture("en");
                    // Formatting numbers, dates, etc.
                    opts.SupportedCultures = supportedCultures;
                    // UI strings that we have localized.
                    opts.SupportedUICultures = supportedCultures;
                });

            services.Configure<IdentityOptions>(options =>
            {
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // Default User settings.
                options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;

                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            });
            services.ConfigureApplicationCookie(options =>
            {

                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessDenied";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
                dbInitializer.Initialize();
                dbInitializer.SeedData();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRequestLocalization(app.ApplicationServices
                .GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);


            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "Admin",
                    "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );                
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseRequestLocalization();
        }
    }
}
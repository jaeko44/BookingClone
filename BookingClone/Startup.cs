using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookingClone.Data;
using BookingClone.Services;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace BookingClone
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
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                });

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context, IServiceProvider serviceProvider)
        {

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc();
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //context.Database.Migrate();
            var adminUser = new ApplicationUser
            {

                UserName = Configuration["AppSettings:Email"],
                Email = Configuration["AppSettings:Email"],
            };
            string adminPassword = Configuration.GetSection("AppSettings:Password").Value;
            CreateRoles(serviceProvider, adminUser, adminPassword).Wait();

        }
        public async Task CreateRoles(IServiceProvider serviceProvider, ApplicationUser adminUser, string adminPassword)
        {
            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            bool roleExist = await RoleManager.RoleExistsAsync("Admin");
            if (roleExist) { 
                IdentityResult chkUser = await UserManager.CreateAsync(adminUser, adminPassword);

                if (chkUser.Succeeded)
                {
                   var result1 = await UserManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    await UserManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
            else
            {
                IdentityResult chkUser = await UserManager.CreateAsync(adminUser, adminPassword);
                var roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
                await UserManager.AddToRoleAsync(adminUser, "Admin");

            }
        }
    }
}

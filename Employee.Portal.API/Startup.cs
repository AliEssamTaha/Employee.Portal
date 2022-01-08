using AutoMapper;
using Employee.Portal.API.Middlewares;
using Employee.Portal.Domain.Entities;
using Employee.Portal.Repo;
using Employee.Portal.Service;
using Employee.Portal.Service.AutoMapper;
using Employee.Portal.CoreLib.Config;
using Employee.Portal.Service.Interfaces;
using Employee.Portal.Service.TokenGenerators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace Employee.Portal.API
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
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            }
            ); ;

            services.AddIdentityCore<User>(o =>
            {
                o.User.RequireUniqueEmail = true;

                o.Password.RequireDigit = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireLowercase = false;
                o.Password.RequiredLength = 0;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Employee.Portal.API", Version = "v1" });
            });

            AuthenticationConfiguration authenticationConfiguration = new AuthenticationConfiguration();
            Configuration.Bind("Authentication", authenticationConfiguration);

            services.AddSingleton(authenticationConfiguration);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IAuthenticationService),typeof(AuthenticationService));
            services.AddScoped(typeof(IAccessTokenGenerator),typeof(AccessTokenGenerator));
            services.AddScoped(typeof(IUserService),typeof(UserService));
            services.AddScoped(typeof(IEmployeeService),typeof(EmployeeService));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
            Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.AccessTokenSecret)),
                    ValidIssuer = authenticationConfiguration.Issuer,
                    ValidAudience = authenticationConfiguration.Audience,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee.Portal.API v1"));
            }

            app.UseCors((options) => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseHttpsRedirection();

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

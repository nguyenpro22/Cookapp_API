using Cookapp.Controllers;
using Cookapp.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Cookapp
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
            //Read config DB: 
            //MiniShopDataAccessGlobal.SqlConnectionString = ConfigConnection.MySqlConnectionString(Configuration);
            //MiniShopDataAccessGlobal.SqlCommandTimeout = ConfigConnection.SqlCommandTimeout(Configuration);
            //
            //ConfigAppSetting.LoadConfig(Configuration);
            //
            //
            //string encryptSTR = Security.Encryption.Encrypt("thanhtuan2308");
            //
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials().Build());
            });
            services.AddDbContext<CookingRecipeDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("CookappDB")));
            services.AddControllers();
            services.AddSwaggerGen();
            //
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "thao",
                        ValidAudience = "thao",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtConfig:Secret"]))
                    };
                });
            //services.AddMvc();
        }
       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            
            app.UseSwagger();
            app.UseSwaggerUI();

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

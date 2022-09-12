using EjercicioAxonet.Common.DTOs;
using EjercicioAxonet.DataBase;
using EjercicioAxonet.Domain;
using EjercicioAxonet.Repository;
using EjercicioAxonetAPI.Manager;
using EjercicioAxonetAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioAxonetAPI
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

            services.AddControllers();
            services.AddDbContext<EjercicioAxonetContext>(option =>
                option.UseSqlServer(
                    Configuration.GetConnectionString("EjercicioAxonetSqlServer"),
                    ops => ops.MigrationsHistoryTable("__EFMigrationsHistory", "EjercicioAxonetDB")
                    )
                );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EjercicioAxonetAPI", Version = "v1" });
            });

            #region AutoMapper       
            services.AddAutoMapper(Configuration => {
                Configuration.CreateMap<ProveedorDTO, Proveedores>().ReverseMap();
                Configuration.CreateMap<Proveedores, ProveedorDTO>().ReverseMap();

                Configuration.CreateMap <MonedaDTO, Monedas>().ReverseMap();
                Configuration.CreateMap<Monedas, MonedaDTO>().ReverseMap();

                Configuration.CreateMap<ReciboDTO, Recibos>().ReverseMap();
                Configuration.CreateMap<Recibos, ReciboDTO>().ReverseMap();
            });
            #endregion

            #region Repositorios
            services.AddScoped<ProveedoresRepository>();
            services.AddScoped<MonedasRepository>();
            services.AddScoped<RecibosRepository>();
            #endregion

            #region Services
            services.AddScoped<CuentasService>();
            services.AddScoped<ProveedoresService>();
            services.AddScoped<MonedasService>();
            services.AddScoped<RecibosService>();
            #endregion

            services.AddScoped<JwtManager>();
            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<EjercicioAxonetContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters{
                ValidateIssuer =false,
                ValidateAudience=false,
                ValidateLifetime=true,
                ValidateIssuerSigningKey=true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["LLavejwt"])),
                ClockSkew = TimeSpan.Zero
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EjercicioAxonetAPI v1"));
            }

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

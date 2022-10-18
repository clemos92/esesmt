using AutoMapper;
using FluentValidation.AspNetCore;
using ESESMT.Application.AutoMapper;
using ESESMT.Application.Filters;
using ESESMT.Application.Middlewares;
using ESESMT.Infra.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ESESMT.Application
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
            var mvcBuilder = services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add<NotificationFilter>();
            });

            mvcBuilder.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<Startup>();
                options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });

            mvcBuilder.AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            //To avoid the MultiPartBodyLength error
            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue; options.MultipartBodyLengthLimit = int.MaxValue; options.MemoryBufferThreshold = int.MaxValue;
            });

            AddCors(services);
            AddMySqlDependency(services);
            AddAutoMapperDependency(services);

            services.AddNativeDependencies();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            this.UseCors(app);

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ESESMT V1");
            });

            app.UseAuthentication();
            app.UseAuthorization();
        }

        #region Private Methods

        private void UseCors(IApplicationBuilder app)
        {
            app.UseCors(
                options => options.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );
        }

        private void AddCors(IServiceCollection services)
        {
            services.AddCors(option =>
            {
                option.AddPolicy("AllowAnyOrigin",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        private void AddAutoMapperDependency(IServiceCollection services)
        {
            var mapperConfig = AutoMapperConfiguration.RegisterMappings();
            mapperConfig.AssertConfigurationIsValid(); //Is OK!
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAutoMapper(typeof(Startup));
        }

        private void AddMySqlDependency(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("default");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySQL(connectionString, opt =>
                {
                    opt.CommandTimeout(180);
                });
            });
        }
        #endregion
    }
}
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sat.Recruitment.Application.Mappings;
using Sat.Recruitment.Application.User.Add;
using Sat.Recruitment.Domain.Contracts;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Interfaces;
using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Domain.Repositories;
using Sat.Recruitment.Domain.Services;
using Sat.Recruitment.Persistence.Repositories;

namespace Sat.Recruitment.Api
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
            services.AddSwaggerGen();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));

            AddConfigurations(services);
            AddServices(services);

            services.AddAutoMapper(typeof(UserProfile));
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IRequestHandler<AddUserRequest, IOperationResult<UserDto>>, AddUserHandler>();
            services.AddScoped<IUnitOfWork, CsvUnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(CsvRepository<>));
        }

        private void AddConfigurations(IServiceCollection services)
        {
            FileConfiguration fileMetadata = BuildFileConfiguration();
            services.AddScoped(metadata => fileMetadata);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private FileConfiguration BuildFileConfiguration() => Configuration.GetSection("FileConfiguration").Get<FileConfiguration>();
    }
}

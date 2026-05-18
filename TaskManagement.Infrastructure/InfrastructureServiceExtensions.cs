using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.Services;
using TaskManagement.Infrastructure.Data;
using TaskManagement.SharedKernel.File;

namespace TaskManagement.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            ConfigurationManager config,
            ILogger logger)
        {
            bool isWindows = OperatingSystem.IsWindows();
            bool forceSqlServer = Environment.GetEnvironmentVariable("USE_SQL_SERVER") == "true";

            string? connectionString = config.GetConnectionString("taskmanagement")
                                        ?? ((isWindows || forceSqlServer) ? config.GetConnectionString("DefaultConnection") : null)
                                        ?? config.GetConnectionString("SqliteConnection");
            if (connectionString == null)
                throw new Exception("connectionString cannot be empty");

            services.AddScoped<EventDispatcherInterceptor>();
            services.AddScoped<IDomainEventDispatcher, MediatorDomainEventDispatcher>();

            services.AddDbContext<AppDbContext>((provider, options) =>
            {
                var eventDispathInterceptor = provider.GetRequiredService<EventDispatcherInterceptor>();

                if(config.GetConnectionString("taskmanagement") != null ||
                    ((isWindows || forceSqlServer) && config.GetConnectionString("DefaultConnection") != null){
                    options.UseSqlServer(connectionString);
                }
                else
                {
                    options.UseSqlite(connectionString);
                }

                options.AddInterceptors(eventDispathInterceptor);
            });

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
                    .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>))
                    .AddScoped<IFileStorageService, LocalFileStorageService>()
                    .AddScoped<IUnitOfWork, UnitOfWork>()
                    .AddScoped<ISendNotificationService, SendNotificationService>();

            logger.LogInformation("Infrastruture Service registered");

            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using ESESMT.Infra.Shared.Common;
using ESESMT.Infra.Shared.Interfaces;
using ESESMT.Infra.Shared.Notifications;
using ESESMT.Domain.Interfaces;
using ESESMT.Infra.CrossCutting.Loggers;
using ESESMT.Infra.Data.Repository;
using ESESMT.Service.Services;
using ESESMT.Service.Validators;

namespace ESESMT.Application
{
    public static class NativeDependencyInjector
    {
        public static void AddNativeDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IApplicationLogger, ApplicationLogger>();
            services.AddSingleton<IHttpRequests, HttpRequests>();
            services.AddScoped<NotificationContext>();
            services.AddServices();
            services.AddRepositories();
            services.AddValidators();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IChecklistTypeService, ChecklistTypeService>();
            services.AddScoped<IChecklistService, ChecklistService>();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IChecklistTypeRepository, ChecklistTypeRepository>();
            services.AddScoped<IChecklistRepository, ChecklistRepository>();
        }

        private static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<ChecklistTypeValidator>();
            services.AddScoped<ChecklistValidator>();
        }
    }
}

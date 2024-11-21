using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure;

public static class DependecyInjection
{
    public static IServiceCollection AddInfrastrucutreServices(this IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        return services;
    }
    public static WebApplication UseApiServices(this WebApplication app) { return app; }
}

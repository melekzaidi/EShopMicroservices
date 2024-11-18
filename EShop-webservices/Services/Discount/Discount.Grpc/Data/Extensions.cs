﻿using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app) { 
        using var scope=app.ApplicationServices.CreateScope();
            using DiscountContext dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
            dbContext.Database.MigrateAsync();
            return app;
        }
    }
}

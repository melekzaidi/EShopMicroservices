using Ordering.Api;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var app = builder.Build();
builder.Services.AddApplicationServices().AddInfrastrucutreServices(builder.Configuration).AddApiServices();



app.Run();

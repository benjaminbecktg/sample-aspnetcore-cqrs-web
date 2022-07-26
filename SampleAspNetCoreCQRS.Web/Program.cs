using SampleAspNetCoreCQRS.Business;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MediatR;
using SampleAspNetCoreCQRS.Business.Services;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
builder.Host.UseEnvironment(environment);
builder.WebHost.UseEnvironment(environment);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Entity Framework
builder.Services.AddDbContext<IDataContext, DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataContextEntities"));
});

builder.Services.AddScoped<SampleAspNetCoreCQRS.Business.IContext>(x => new SampleAspNetCoreCQRS.Web.Context(x.GetService<IDataContext>()));
builder.Services.AddScoped<SampleAspNetCoreCQRS.Web.IContext>(x => new SampleAspNetCoreCQRS.Web.Context(x.GetService<IDataContext>()));

builder.Services.AddScoped<IPeopleService, PeopleService>();

// Register Assembly to Mediatr
builder.Services.AddMediatR(typeof(SampleAspNetCoreCQRS.Business.Features.People.Queries.GetPersonByIdQuery).GetTypeInfo().Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DataContext>();
    context.Database.EnsureCreated();
}

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.UseHttpsRedirection();

app.Run();

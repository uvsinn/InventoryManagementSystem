
//The first line creates an object of WebApplicationBuilder with preconfigured defaults using the CreateBuilder() method.
using CommonServices;
using InventoryManagementSystem.CustomMiddleware;
using InventoryManagementSystem.DataAccessLayer;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Models.Interfaces;
using Microsoft.AspNetCore.Builder;
using HttpContext = Microsoft.AspNetCore.Http.HttpContext;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();

//Registering custom middleware
builder.Services.AddTransient<CustomMiddleware>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<Imanager<Inventory>, Manager<Inventory>>();
builder.Services.AddScoped<Imanager<Orderlist>, Manager<Orderlist>>();
builder.Services.AddScoped<IManagingInventoryDL, ManagingInventoryDL>();

var app = builder.Build();
//var Inventory = app.Services.GetService<Imanager<Inventory>>();

app.Configure();



// Configure the HTTP request pipeline.

//  CONTROLLERS

//app.Use(async (HttpContext context, RequestDelegate next) =>
//{
//    await context.Response.WriteAsync("Hello guys /n");
//    await next(context);
//});
//app.Use(async (HttpContext context, RequestDelegate next) =>
//{
//    await context.Response.WriteAsync("Use swagger to make api calls/n");
//    await next(context);
//});

//app.UseMiddleware<CustomMiddleware>();

//app.Run(async (HttpContext context) =>
//{
//    await context.Response.WriteAsync("best of luck");
//});


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(OPTIONS=>
OPTIONS.WithOrigins("http://localhost:4200")
.AllowAnyMethod()
.AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();


app.Run();


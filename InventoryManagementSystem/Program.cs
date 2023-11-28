//The first line creates an object of WebApplicationBuilder with preconfigured defaults using the CreateBuilder() method.
using InventoryManagementSystem.CustomMiddleware;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();

//Registering custom middleware
builder.Services.AddTransient<CustomMiddleware>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello guys /n");
    await next(context);
});
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Use swagger to make api calls/n");
    await next(context);
});

app.UseMiddleware<CustomMiddleware>();

app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("best of luck");
});
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();

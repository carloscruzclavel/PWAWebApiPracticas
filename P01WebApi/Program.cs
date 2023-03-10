using Microsoft.EntityFrameworkCore;
using P01WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// base de datos conexxion

builder.Services.AddDbContext<equiposContext>(option =>
        option.UseSqlServer(
            builder.Configuration.GetConnectionString("equiposDbConnection")
       )
    );
// base de datos conexxion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

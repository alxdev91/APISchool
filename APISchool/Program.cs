using Microsoft.EntityFrameworkCore; //se agrega
using APISchool.Models;             //se agrega
using System.Text.Json.Serialization;//se agrega para utilizar el referenceshandler,para eliminar referencias ciclicas

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//se agrega
builder.Services.AddDbContext<DB_API_SCHOOLContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("StringSQL")));

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Repository;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("DefaultConnection");

// Para Migration: PM> Add-Migration NomeMigration -Project NLX.DA

builder.Services.AddDbContext<NZWalksDbContext>(options =>
         options.UseMySql(conn, ServerVersion.AutoDetect(conn), mysql => mysql.UseNetTopologySuite()));

builder.Services.AddScoped<IRegionRepository, RegionRepository>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add services to the container.

builder.Services.AddControllers();
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

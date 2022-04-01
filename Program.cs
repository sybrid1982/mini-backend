using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiniBackend.Models;
using MiniBackend.Repositories;
using UploadFilesServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "MiniBackend",
        Version = "v1"
    });
});

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options => 
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
            builder => 
            {
                builder.WithOrigins(
                    "http://localhost:8080",
                    "https://localhost:8080",
                    "https://salmon-meadow-0da63b00f.1.azurestaticapps.net"
                ).AllowAnyHeader()
                .AllowAnyMethod();
            }
        );
    });

// Connect to db
builder.Services.AddDbContext<MiniContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BackendMinisContext")));
builder.Services.AddScoped<IMinisRepository, SqlServerDbMinisRepository>();

// connect to blob
builder.Services.AddScoped<IUploadService, UploadService>();


var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

// string path = Path.Combine(builder.Environment.ContentRootPath, "Images");

// Directory.CreateDirectory(path + "/BoxArt");
// Directory.CreateDirectory(path + "/MiniPictures");

// app.UseStaticFiles(new StaticFileOptions
// {
//     FileProvider = new PhysicalFileProvider(path),
//     RequestPath = "/Images"
// });

app.UseSwagger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

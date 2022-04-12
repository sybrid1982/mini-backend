using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
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
builder.Services.AddDbContext<MiniContext>(options => {
    if(builder.Configuration["DOTNET_RUNNING_IN_CONTAINER"] != "true") {
        try { 
            options.UseSqlServer(builder.Configuration.GetConnectionString("BackendMinisContext"));
        } catch (Exception ex) {
            Console.WriteLine("TRIED TO USE DEFAULT CONNECTION STRING FROM SECRETS AND THREW EXCEPTION");
            Console.WriteLine(ex);
        }
    } else {
        if(builder.Configuration["ASPNETCORE_ENVIRONMENT"] == "DOCKER_DEV") {
            try {
                var connection = @"Server=db;Database=master;User=sa;Password=YourStrong@Passw0rd;";
                options.UseSqlServer(connection);
            } catch (Exception ex) {
                Console.WriteLine("TRIED AND FAILED TO CONNECT TO DOCKER CONTAINER SQL SERVER");
                Console.WriteLine(ex);
            }
        } else {
            try {
                string connectionString = "Server=" + builder.Configuration["dbServer"] + "Initial Catalog=" + builder.Configuration["dbInitialCatalog"] + "Persist Security Info=False;User ID=" + builder.Configuration["dbUser"] + "Password=" + builder.Configuration["dbPassword"] + "MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                options.UseSqlServer(connectionString);
            } catch (Exception ex) {
                Console.WriteLine("TRIED TO BUILD STRING FROM ENV VARIABLES AND THREW EXCEPTION");
                Console.WriteLine(ex);
            }
        }
    }
});

builder.Services.AddScoped<IMinisRepository, SqlServerDbMinisRepository>();

// connect to blob outside of DOCKER_DEV
if(builder.Configuration["ASPNETCORE_ENVIRONMENT"] != "DOCKER_DEV")
    builder.Services.AddScoped<IUploadService, UploadService>();
else
    builder.Services.AddScoped<IUploadService, FakeUploadService>();

var app = builder.Build();

try {
    DatabaseManagementService.MigrationInitialisation(app);
} catch {
    Console.WriteLine("Program.cs: Failed to migrate");
}

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

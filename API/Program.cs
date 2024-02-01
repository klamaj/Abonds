using System.Reflection;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add Loger
Log.Logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "Abonds API - V1",
                Version = "v1"
            }
        );

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
        var filePath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(filePath);
    }
);

// Use Serilog
builder.Host.UseSerilog();

var app = builder.Build();

app.UseCors(
    options => options.WithOrigins("http://localhost:4200").AllowAnyMethod()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// UseSaticFiles
app.UseStaticFiles();

app.UseHttpsRedirection();

// JWT Auth
app.UseAuthentication();
app.UseAuthorization();

// Use Controller instead of minimalApi
app.MapControllers();

app.Run();
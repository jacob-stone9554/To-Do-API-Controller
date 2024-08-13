using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ToDoAPI_Controller.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Items") ?? "Data Source=Items.db";

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSqlite<AppDbContext>(connectionString); // Add the SQLite database provider to the container, making it accessible
builder.Services.AddSwaggerGen(c =>
{
    // Add basic documentation and info about the api to swagger
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "To-Do API",
        Description = "A simple to-do list API",
        Version = "v1"
    });
});

/*
 * Add CORS so that the API can send/receive data from the front-end,
 *      only from allowed, specified hosts.
 *      
 * Please note, any origin is currently allowed to request resources.
 * In a case where resources should be non-public, restrict to only allowed origins.
 */
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AnyOrigin",
        cfg =>
        {
            cfg.AllowAnyOrigin();
            cfg.AllowAnyHeader();
            cfg.AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Controller To-Do API V1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AnyOrigin"); // Use the AnyOrigin policy
app.UseAuthorization();
app.MapControllers();

app.Run();

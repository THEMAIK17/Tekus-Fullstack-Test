using Microsoft.EntityFrameworkCore;
using Tekus.Application.Interfaces;
using Tekus.Application.Services;
using Tekus.Domain.Interfaces;
using Tekus.Infraestructure.Data;
using Tekus.Infraestructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// --- Register Services  ---

//  Configure the DbContext
builder.Services.AddDbContext<TekusDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// .NET automatically reads from appsettings.json AND user secrets.


// This tells .NET how to "build" the classes.
builder.Services.AddScoped<IProviderRepository, ProviderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitWork.UnitOfWork>();
builder.Services.AddScoped<IProviderService, ProviderService>();


//  Add CORS Policy (to allow React to call the API)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") 
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


//  Add standard API services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Fixes the infinite loop error (object cycles).
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configure The Middleware ---
var app = builder.Build();

// Show Swagger UI only in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use the CORS policy I defined
app.UseCors("AllowReactApp");

app.UseAuthorization();

// Map the controllers 
app.MapControllers();

app.Run();
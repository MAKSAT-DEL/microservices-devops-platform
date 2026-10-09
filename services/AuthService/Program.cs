using AuthService.Data;
using AuthService.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Prometheus;
using OpenTelemetry;
using OpenTelemetry.Trace;                        

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddOtlpExporter());

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// PostgreSQL DB Context Setup
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<TokenService>();

var app = builder.Build();

// ===== OBSERVABILITY 
app.UseMetricServer();                     
app.UseHttpMetrics();                     

// Veritabanı Otomatik Oluşturma
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.WithTitle("Auth Service API")
           .WithTheme(ScalarTheme.Purple);
});

app.UseAuthorization();
app.MapControllers();

app.Run();
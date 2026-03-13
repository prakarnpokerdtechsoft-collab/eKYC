
using EKYCWebhook.Entity;
using EKYCWebhook.Entity.Data;
using EKYCWebhook.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ============================
// SERILOG CONFIG
// ============================

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // Render จะอ่าน log จาก console
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// ============================
// DATABASE CONFIG
// ============================

var connectionString = builder.Configuration.GetConnectionString("AppContextConnection")
    ?? throw new InvalidOperationException("Connection string 'AppContextConnection' not found.");

builder.Services.AddDbContext<EKYCWebhookContext>(options =>
    options.UseSqlServer(connectionString, sqlOption =>
        sqlOption.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null)));

builder.Services.AddIdentity<EKYCWebhookUsercs, Role>()
    .AddEntityFrameworkStores<EKYCWebhookContext>();

builder.Services.AddScoped<DocumentPipelineService>();

// ============================
// SERVICES
// ============================

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<EkycService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// ============================
// SWAGGER
// ============================

app.UseSwagger();
app.UseSwaggerUI();

// ============================
// REQUEST LOGGING MIDDLEWARE
// ============================

app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

    context.Request.EnableBuffering();

    string body = "";

    if (context.Request.ContentLength > 0)
    {
        using var reader = new StreamReader(
            context.Request.Body,
            Encoding.UTF8,
            leaveOpen: true);

        body = await reader.ReadToEndAsync();

        context.Request.Body.Position = 0;
    }

    logger.LogInformation(
        "Incoming Request | Method: {method} | Path: {path} | IP: {ip} | Body: {body}",
        context.Request.Method,
        context.Request.Path,
        context.Connection.RemoteIpAddress,
        body
    );

    await next();
});

// ============================
// PIPELINE
// ============================
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllers();

app.Run();


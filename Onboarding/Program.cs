using Microsoft.AspNetCore.Diagnostics;
using Onboarding.Extensions;
using Onboarding.Extensions.Persistence.Onboarding;
using Onboarding.Middleware;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// เพิ่ม CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// ===================== SERILOG =====================
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
builder.Logging.ClearProviders(); // ล้าง default log

// ===================== SERVICES =====================
builder.Services.AddOnboarding(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

var app = builder.Build();

// ===================== EXCEPTION HANDLER (ต้องอยู่บนสุด) =====================
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        logger.LogError(exception, "Unhandled Exception");

        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Internal Server Error");
    });
});

var basePath = !app.Environment.IsDevelopment() ? "/ON_API" : "";

if (!string.IsNullOrEmpty(basePath))
{
    app.UsePathBase(basePath);
}

// ===================== SWAGGER =====================
app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint($"{basePath}/openapi/v1.json", "v1");
    options.DefaultModelsExpandDepth(-1);
});

// ===================== SERILOG REQUEST (STANDARD LOG) =====================
app.UseSerilogRequestLogging();

// ===================== CUSTOM REQUEST/RESPONSE LOG =====================
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

    // ===== SKIP บาง endpoint =====
    var path = context.Request.Path.Value?.ToLower();
    if (path != null && (path.Contains("swagger") || path.Contains("health")))
    {
        await next();
        return;
    }

    // ===== REQUEST =====
    context.Request.EnableBuffering();

    string requestBody = "";

    if (context.Request.ContentLength > 0 &&
        context.Request.ContentType != null &&
        context.Request.ContentType.Contains("application/json") &&
        context.Request.ContentLength < 1024 * 10) // 10 KB
    {
        using var reader = new StreamReader(
            context.Request.Body,
            Encoding.UTF8,
            leaveOpen: true);

        requestBody = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0;

        requestBody = MaskSensitive(requestBody);
    }

    // ===== RESPONSE =====
    var originalBodyStream = context.Response.Body;
    using var responseBody = new MemoryStream();
    context.Response.Body = responseBody;

    // ===== EXECUTE =====
    await next();

    // ===== READ RESPONSE =====
    string responseText = "";

    if (responseBody.Length < 1024 * 20) // 20 KB
    {
        responseBody.Seek(0, SeekOrigin.Begin);
        responseText = await new StreamReader(responseBody).ReadToEndAsync();
        responseBody.Seek(0, SeekOrigin.Begin);

        responseText = MaskSensitive(responseText);
    }

    // ===== LOG =====
    logger.LogInformation(
        "HTTP {method} {path} | Status: {statusCode} | IP: {ip} | Req: {req} | Res: {res}",
        context.Request.Method,
        context.Request.Path,
        context.Response.StatusCode,
        context.Connection.RemoteIpAddress,
        requestBody,
        responseText
    );

    // ===== COPY BACK =====
    await responseBody.CopyToAsync(originalBodyStream);
});

app.UseCors("AllowSpecificOrigin");

app.UseMiddleware<ErrorHandlerMiddleware>();

// ===================== PIPELINE =====================
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.MapControllers();
app.Run();

// ===================== HELPER =====================
static string MaskSensitive(string input)
{
    if (string.IsNullOrEmpty(input)) return input;

    // mask otp
    input = System.Text.RegularExpressions.Regex.Replace(
        input,
        "\"otp\"\\s*:\\s*\".*?\"",
        "\"otp\":\"***\"",
        System.Text.RegularExpressions.RegexOptions.IgnoreCase
    );

    // mask password
    input = System.Text.RegularExpressions.Regex.Replace(
        input,
        "\"password\"\\s*:\\s*\".*?\"",
        "\"password\":\"***\"",
        System.Text.RegularExpressions.RegexOptions.IgnoreCase
    );

    return input;
}
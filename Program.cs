using EKYCWebhook.Entity;
using EKYCWebhook.Entity.Data;
using EKYCWebhook.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AppContextConnection")
    ?? throw new InvalidOperationException("Connection string 'AppContextConnection' not found.");

builder.Services.AddDbContext<EKYCWebhookContext>(options =>
    options.UseSqlServer(connectionString, sqlOption =>
        sqlOption.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null)));

builder.Services.AddIdentity<EKYCWebhookUsercs, Role>()
    .AddEntityFrameworkStores<EKYCWebhookContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<EkycService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
using Billing.Services.Implementation;
using FluentValidation.AspNetCore;
using Serilog;
using Services.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
string logFileName = $"log-{DateTime.Now:yyyyMMddHHmm}.txt";

Log.Logger = new LoggerConfiguration()
    .WriteTo.File($"{desktopPath}\\logs\\{logFileName}")
    .CreateLogger();

builder.Services.AddLogging(configure => configure.AddSerilog());
builder.Services.AddStrategies(typeof(BillingService).Assembly);
builder.Services.RegisterServices();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
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

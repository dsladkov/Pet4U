
using Pet4U.Application;
using Pet4U.Infrastructure;
using FluentValidation.AspNetCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Pet4U.API.Validation;
using Microsoft.EntityFrameworkCore;
using Pet4U.API.Extensions;
using Pet4U.API.Middleware;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq") 
                ?? throw new ArgumentNullException("Seq"))
    .Enrich.WithThreadId()
    .WriteTo.File("log/logs.log", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    //.CreateLogger(); // <-- Change this line!
    .CreateBootstrapLogger();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


builder.Services.AddInfraStructure(builder.Configuration);

builder.Services.AddApplication();

builder.Services.AddValidation();

// builder.Services.AddHttpLogging( o =>
// {
//     o.CombineLogs = true;
// });

builder.Services.AddTransient<IConfiguration>(sp =>
{
    IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
    configurationBuilder.AddJsonFile("appsettings.json");
    return configurationBuilder.Build();
});

builder.Services.AddSerilog((services, lc) => lc
    .ReadFrom.Configuration(builder.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq") 
                ?? throw new ArgumentNullException("Seq"))
    .WriteTo.Console());



var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseExceptionMiddleware();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await app.ApplyMigrations();
}

//app.UseHttpLogging(); //логирование запросов

app.UseAuthorization();

app.MapControllers();


//code below is just for example how to use extension method Use with delegate
// app.Use( async (context, next) => 
// {
//     //logic before next middlerware
//     System.Console.WriteLine(context.Request.Path + "Middleware 1 start");
//     await next.Invoke();
//     System.Console.WriteLine(context.Request.Path + "Middleware 1 end");
// });

// app.Use( async (context, next) => 
// {
    
//     //logic before next middlerware
//     System.Console.WriteLine(context.Request.Path + "Middleware 2 start");
//     await next.Invoke(context);
//     System.Console.WriteLine(context.Request.Path + "Middleware 2 end");
// });

app.Run();
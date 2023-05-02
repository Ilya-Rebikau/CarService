using CarService.DAL.Configuration;
using CarService.UserAPI.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddUserApiServices(builder.Configuration);

var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hostsettings.json", true)
                .AddCommandLine(args)
                .Build();

builder.Configuration.AddConfiguration(config);
builder.Host.UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Users API v1");
    });
}

app.UseSerilogRequestLogging();

app.UseRouting();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
using SMSService.Data;
using SMSService.Data.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using SMSService.API.Services;
using SMSSerivce.API.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMSSerivce.API.Dtos;
using SMSSerivce.API.Models;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder()
                            .SetBasePath(builder.Environment.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                            .AddEnvironmentVariables();


builder.Configuration.AddConfiguration(configurationBuilder.Build());



// Add services to the container.
{
    var services = builder.Services;
    services.AddCors();
    services.AddControllers()
        .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
        .ConfigureApiBehaviorOptions(options =>
        {
            //options.SuppressModelStateInvalidFilter = true;
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var modelState = actionContext.ModelState.Values;
                var errors = new List<ResponseDto>();
                foreach (var state in modelState)
                {

                    foreach (var error in state.Errors)
                    {

                        errors.Add(new ResponseDto() { Message = "", Error = error.ErrorMessage });
                    }
                }
                return new BadRequestObjectResult(errors);
            };
        });
    services.Configure<AppSetting>(builder.Configuration.GetSection("AppSettings"));
    services.AddMemoryCache();

    services.AddDbContext<SMSDataContext>( option => 
            option.UseNpgsql(builder.Configuration.GetConnectionString("SMSServiceDb"))
        );
    services.AddScoped<IAccountService, AccountService>();
    services.AddScoped<IPhoneNumberService, PhoneNumberService>();
    services.AddScoped<IUserService, UserService>();
    
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}
var app = builder.Build();

// Configure the HTTP request pipeline.
{
    app.UseHttpsRedirection();
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // custom basic auth middleware
    app.UseMiddleware<BasicAuthMiddleware>();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

   

    //app.UseAuthorization();

    app.MapControllers();
}
app.Run();

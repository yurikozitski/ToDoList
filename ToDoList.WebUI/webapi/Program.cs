using ToDoList.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ToDoList.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDoList.Infrastructure.Mediator.Commands;
using ToDoList.Infrastructure.Repositories;
using ToDoList.Core.AuthInterfaces;
using ToDoList.Infrastructure.Auth;
using ToDoList.Core.RepositoryInterfaces;
using webapi.Middleware;
using ToDoList.Core.ServiceInterfaces;
using ToDoList.Infrastructure.Services;
using FluentValidation;
using ToDoList.Infrastructure.Mediator.Validators;
using NLog;
using NLog.Web;
using Microsoft.AspNetCore.Mvc;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(
    new WebApplicationOptions { WebRootPath = "dist" });

    // Add services to the container.
    builder.Services.AddDbContext<ApplicationContext>(options =>
					options.UseSqlServer(builder.Configuration.GetConnectionString("Development")));

	builder.Services.AddIdentity<User, IdentityRole>(opts =>
	{
		opts.Password.RequiredLength = 8;
		opts.Password.RequireNonAlphanumeric = false;
		opts.Password.RequireLowercase = false;
		opts.Password.RequireUppercase = false;
		opts.Password.RequireDigit = false;
	}).AddEntityFrameworkStores<ApplicationContext>();

	builder.Services.AddAuthorization();
	builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidIssuer = "Issuer",
				ValidateAudience = true,
				ValidAudience = "Audience",
				ValidateLifetime = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token"]!)),
				ValidateIssuerSigningKey = true,
			};
		});

	builder.Services.AddControllers();

	builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<RegistrationHandler>(
	));

	builder.Services.AddValidatorsFromAssemblyContaining<RegistrationCommandValidator>();

	builder.Services.AddTransient<IJwtGenerator, JwtGenerator>();
	builder.Services.AddTransient<IUserRepository, UserRepository>();
	builder.Services.AddTransient<ITaskRepository, TaskRepository>();
	builder.Services.AddSingleton<IImageValidator, ImageValidator>();

	builder.Services.AddCors();

	// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();

	builder.Services.Configure<ApiBehaviorOptions>(options =>
	{
		options.SuppressModelStateInvalidFilter = true;
	});

	// NLog: Setup NLog for Dependency injection
	builder.Logging.ClearProviders();
	builder.Host.UseNLog();

	var app = builder.Build();

	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
		app.UseCors(
				options => options.WithOrigins("https://localhost:5173").WithOrigins("https://192.168.0.104:5173")
				.AllowAnyMethod()
				.AllowAnyHeader()
			);
	}
	else
	{
        app.UseDefaultFiles();
        app.UseStaticFiles();
    }

	app.UseHttpsRedirection();

	app.UseRouting();

	app.UseAuthentication();
	app.UseAuthorization();

	app.UseMiddleware<ErrorHandlingMiddleware>();
	app.MapControllers();

    app.MapFallbackToFile("index.html");

    app.Run();
}
catch (Exception exception)
{
	logger.Error(exception, "Stopped program because of exception");
	throw;
}
finally
{
	NLog.LogManager.Shutdown();
}



using ToDoList.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ToDoList.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MediatR;
using System.Reflection;
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


// Early init of NLog to allow startup and exception logging, before host is built
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
	var builder = WebApplication.CreateBuilder(args);

	// Add services to the container.
	builder.Services.AddDbContext<ApplicationContext>(options =>
					options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token"])),
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

	// NLog: Setup NLog for Dependency injection
	builder.Logging.ClearProviders();
	builder.Host.UseNLog();

	var app = builder.Build();

	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

	app.UseHttpsRedirection();

	app.UseRouting();

	app.UseCors(
			options => options.WithOrigins("https://localhost:5173").WithOrigins("https://192.168.0.104:5173")
			.AllowAnyMethod()
			.AllowAnyHeader()
		);

	app.UseAuthentication();
	app.UseAuthorization();

	app.UseMiddleware<ErrorHandlingMiddleware>();
	app.MapControllers();

	app.Run();
}
catch (Exception exception)
{
	// NLog: catch setup errors
	logger.Error(exception, "Stopped program because of exception");
	throw;
}
finally
{
	// Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
	NLog.LogManager.Shutdown();
}



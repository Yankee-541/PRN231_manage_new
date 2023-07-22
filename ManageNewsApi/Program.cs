using BusinessLogic.Models;
using DataAccess.DAOs;
using DataAccess.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Repositories.Business;
using Repositories.Interface;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddScoped<IUserBusiness, UserBusiness>();
builder.Services.AddScoped<IUserDAO, UserDAO>();
builder.Services.AddScoped<INewsBusiness, NewsBusiness>();
builder.Services.AddScoped<INewsDAO, NewsDAO>();
builder.Services.AddScoped<ICategoriesDAO, CategoriesDAO>();
builder.Services.AddScoped<ICategoriesBusiness, CategoriesBusiness>();
builder.Services.AddScoped<IAuthBusiness, AuthBusiness>();
builder.Services.AddScoped<IAccountDAO, AccountDAO>();

#region Add Authentication Service 

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	// Tùy chọn này tạo ra một đối tượng TokenValidationParameters để cấu hình các thông số để xác thực và giải mã JWT.
	options.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
	};
});

#endregion

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyStoreDB")));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(builder =>
builder.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
	IdentityModelEventSource.ShowPII = true;
}
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
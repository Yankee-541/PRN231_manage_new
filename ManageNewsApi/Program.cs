using BusinessLogic.Models;
using DataAccess.DAOs;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using Repositories.Business;
using Repositories.Interface;

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
}
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();


app.Run();
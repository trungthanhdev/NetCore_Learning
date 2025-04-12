using Microsoft.EntityFrameworkCore;
using NetCore_Learning.Data;
using NetCore_Learning.Interfaces;
using NetCore_Learning.Repository;
using Microsoft.AspNetCore.Mvc;
using NetCore_Learning.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using DotNetEnv;
using System.Text;
using NetCore_Learning.Services;



var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();
// Console.WriteLine("Issuer from Env.Get: " + Environment.GetEnvironmentVariable("JWT__Issuer"));
// Console.WriteLine("audience from Env.Get: " + Environment.GetEnvironmentVariable("JWT__Audience"));
// Console.WriteLine("signingKey from Env.Get: " + Environment.GetEnvironmentVariable("JWT__SigningKey"));

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add dbcontext to db to be built
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//add NewtonsoftJson
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddIdentity<AppUser, IdentityRole>(option =>
{
    option.Password.RequiredUniqueChars = 1;
    option.Password.RequireDigit = true;
    option.Password.RequireUppercase = true;
    option.Password.RequireNonAlphanumeric = true;
    option.Password.RequiredLength = 12;
}).AddEntityFrameworkStores<ApplicationDBContext>();


var issuer = Environment.GetEnvironmentVariable("JWT__Issuer");
var audience = Environment.GetEnvironmentVariable("JWT__Audience");
var signingKey = Environment.GetEnvironmentVariable("JWT__SigningKey");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

//add Interface and Repository
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();

// builder.WebHost.UseUrls("http://0.0.0.0:5001");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


//Map de Swagger hoat dong
app.MapControllers();
app.Run();

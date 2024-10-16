using AccessLayerDLL.Data;
using AccessLayerDLL.Interfaces;
using AccessLayerDLL.Models;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Session;
using AccessLayerDLL.Repositories;
using BusinessLayer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddControllers(); // Keep this for API-like behavior if needed

// dbcontext and connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null);
    }));

// Add Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


//builder.Services.AddSession();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


// Add Authentication and JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey is missing from configuration")))
    };
});

// Add Authorization
builder.Services.AddAuthorization();


// Register Repositories and Services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITaskListTemplateRepository, TaskListTemplateRepository>();
builder.Services.AddScoped<ITaskListTemplateService, TaskListTemplateService>();

builder.Services.AddScoped<ITemplateGroupRepository, TemplateGroupRepository>();
builder.Services.AddScoped<ITemplateGroupService, TemplateGroupService>();

builder.Services.AddScoped<IGroupTaskRepository, GroupTaskRepository>();
builder.Services.AddScoped<IGroupTaskService, GroupTaskService>();


// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Build the app
var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Show detailed errors in development
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAllOrigins");

app.UseSession();

app.UseMiddleware<TokenMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

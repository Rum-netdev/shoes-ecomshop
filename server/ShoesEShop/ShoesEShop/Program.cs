
using AuthSystemApp.Core.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShoesEShop.Core.Options.Configurations;
using ShoesEShop.Data;
using ShoesEShop.Data.Entities;
using ShoesEShop.Data.Seeds;
using ShoesEShop.Handler.Infrastructures;
using ShoesEShop.Handler.Mapping;
using ShoesEShop.Handler.Services;
using ShoesEShop.Handler.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(config =>
{
    config.UseSqlServer(builder.Configuration.GetConnectionString("AuthSystemAppDefault"));
});
builder.Services.AddIdentity<AppUser, AppRole>(config =>
{
    config.Password.RequireDigit = true;
    config.Password.RequireUppercase = true;
    config.Password.RequiredLength = 8;

    config.User.RequireUniqueEmail = true;
    config.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

JwtConfigurationOption jwtConfig = new JwtConfigurationOption();
builder.Configuration.GetSection(JwtConfigurationOptionSetup.SectionName)
    .Bind(jwtConfig);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(setup =>
    {
        setup.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidAudience = jwtConfig.Audience,
            ValidateAudience = jwtConfig.ValidateAudience,
            ValidIssuer = jwtConfig.Issuer,
            ValidateIssuer = jwtConfig.ValidateIssuer,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecurityKey))
        };
    });

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(IBroker).Assembly));
builder.Services.AddScoped<IBroker, Broker>();
builder.Services.AddScoped<IJwtAuthenticationManager, JwtAuthenticationManager>();
builder.Services.AddAutoMapper(typeof(AppUserProfile).Assembly);

// Registering options
builder.Services.ConfigureOptions<JwtConfigurationOptionSetup>();

// Enabling CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", cfg =>
    {
        cfg.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("AllowAll");
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapDefaultControllerRoute();

// Seed data before run application
using (var scope = app.Services.CreateScope())
{
    var provider = scope.ServiceProvider;
    var userManager = provider.GetRequiredService<UserManager<AppUser>>();
    var roleManager = provider.GetRequiredService<RoleManager<AppRole>>();
    await DataSeeder.SeedAsync(userManager, roleManager);
}


app.Run();
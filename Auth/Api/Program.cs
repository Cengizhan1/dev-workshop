using Application.Interfaces;
using Domain.ConfigModels;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<IAuthService, AuthService>();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Keycloak configuration from appsettings.json
builder.Services.Configure<KeycloakOptions>(builder.Configuration.GetSection("Keycloak"));



// Configure Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Access KeycloakOptions through IOptions
        var keycloakConfig = builder.Configuration.GetSection("Keycloak").Get<KeycloakOptions>();

        options.Authority = keycloakConfig.Authority;
        options.Audience = keycloakConfig.Audience;
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = keycloakConfig.Audience,
            ValidateIssuer = true,
            ValidIssuer = keycloakConfig.Authority
        };
    });

// Authorization configuration
builder.Services.AddAuthorization();

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

app.MapControllers();

app.Run();

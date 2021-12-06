using EnergyHeatMap.Infrastructure;
using EnergyHeatMap.Api.EndpointDefinitions;
using EnergyHeatMap.Api.Extensions;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

var appSettingsSection = builder.Configuration.GetSection("SecuritySettings");
builder.Services.Configure<SecuritySettings>(appSettingsSection);

var dataPathSettings = builder.Configuration.GetSection("DataPaths");
builder.Services.Configure<DataPathSettings>(dataPathSettings);


builder.Services.AddInfrastructure();
builder.Services.AddEndpointDefinitions(typeof(UserEndpointDefinition));
builder.Services.AddCors();

var securitySettings = appSettingsSection.Get<SecuritySettings>();
var secret = Encoding.ASCII.GetBytes(securitySettings.Secret);

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secret),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    .RequireAuthenticatedUser()
    .Build();
});

var app = builder.Build();

app.UseCors(
    options =>
    {
        options.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });

app.UseEndpointDefinitions();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}



app.Run();
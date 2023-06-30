using JwtTokenSample.Infrastructure;
using JwtTokenSample.Models.Repositories;
using JwtTokenSample.Models.Servivces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<DbConnection>(sp => new SqlConnection(configuration.GetConnectionString("F23L034_GestContact_Cqs")));
builder.Services.AddScoped<IAuthRepository, AuthService>();

#region Ajout des services pour JwtToken
//////////////////////////////////////////////////////////////////////////////////////////////
/// /!\ Avant toute chose ajoutez le package Microsoft.AspNetCore.Authentication.JwtBearer ///
//////////////////////////////////////////////////////////////////////////////////////////////
builder.Services.AddScoped<ITokenRepository, TokenService>();
builder.Services.AddAuthorization();
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
#endregion Ajout des services pour JwtToken

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
// Ou si vous voulez activer la gestion du token par Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Veuillez insérer le token JWT précédé de 'Bearer '",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
        //OpenApiSecurityRequirement est de type Dictionary<OpenApiSecurityScheme, IList<string>> 
        { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, Array.Empty<string>() }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Activation de l'authentification et de l'authorization au niveau de l'application
app.UseAuthentication();
app.UseAuthorization();
#endregion Activation de l'authetification et de l'authorization au niveau de l'application

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

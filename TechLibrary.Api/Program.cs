using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TechLibrary.Api.Filters;

const string AUTHENTICATION_TYPE = "Bearer";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// map all the endpoints and generate swagger documentation
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(AUTHENTICATION_TYPE, new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                        Enter 'Bearer' [space] and then your token in the text input below.
                        Example: \'Bearer 12345abcdef\'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = AUTHENTICATION_TYPE,
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = AUTHENTICATION_TYPE,
                },
                Scheme = "oauth2",
                Name = AUTHENTICATION_TYPE,
                In = ParameterLocation.Header,
            },
            new List<string>()
        },
    });
});

// register the exception filter
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

// register the authentication for controllers, isntall JwtBearerDefaults package
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true, // the token can expire, so we need to validate it here
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKey()
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// Swagger is recommended to be used only in development environment
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// duplicating the code from Infrastructure>Security>Tokens>Access
// not recommended to duplicate code like this, this is just for the sake of the example
SymmetricSecurityKey SecurityKey()
{
    // hardcoding the key, not a good practice, just for the sake of the example
    // the key has to have a minimum of 32 characters
    var signInKey = "64aOpmMA7xxmNkGXT0x07ebqQdnPtzlG";

    // in order for the key to be used in the cryptography, it has to be converted to a byte array
    var symmetricKey = Encoding.UTF8.GetBytes(signInKey);

    return new SymmetricSecurityKey(symmetricKey);
}

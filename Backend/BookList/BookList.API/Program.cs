using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using BookList.Data.Repository.Generic;
using BookList.Data.Repository;
using BookList.Data;
using BookList.DomainService.Book;
using BookList.DomainService.Users;
using BookList.DomainService.JwtProvider;
using BookList.DomainService.Authentication;
using FluentValidation;
using BookList.DomainService.DTOs;
using BookList.DomainService.Validation;
using BookList.DomainService;
using BookList.Core;
using BookList.DomainService.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

builder.Services.AddControllers();

builder.Services.AddDbContext<BookListDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookListConnection")),ServiceLifetime.Scoped);

builder.Services.AddScoped<IValidator<BookDto>, BookDtoValidator>();
builder.Services.AddScoped<IValidator<UserLoginRequest>, UserLoginRequestValidator>();



builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

// Add JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings"); // Fetch JWT settings from appsettings.json
var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]!); // Convert secret to byte array

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        ClockSkew = TimeSpan.Zero
    };
});builder.Services.AddAuthorization();

// Register OpenAPI (Swagger) for API documentation
builder.Services.AddEndpointsApiExplorer();  // This is required for OpenAPI/Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "BookList API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {token}'"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Add Cors (Optional, useful for front-end access from different origins)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactBookList", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Enables the Swagger JSON endpoint
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookList API v1");  // Swagger UI endpoint
        c.RoutePrefix = string.Empty;  // Set the Swagger UI to be at the root of the application
    });
}
app.UseCors("AllowReactBookList");

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();


app.MapControllers();

app.Run();

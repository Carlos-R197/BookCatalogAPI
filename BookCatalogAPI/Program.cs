using BookCatalogAPI.Data;
using BookCatalogAPI.Repositories;
using BookCatalogAPI.Repositories.Contracts;
using BookCatalogAPI.Validations;
using BookCatalogAPI.Authentication;
using BookCatalogModels.Dtos;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<BookDbContext>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<GenreCreateDto>, GenreCreateDtoValidator>();
builder.Services.AddScoped<IValidator<AuthorCreateDto>, AuthorCreateDtoValidator>();
builder.Services.AddScoped<IValidator<BookCreateDto>, BookCreateDtoValidator>();
builder.Services.AddScoped<IValidator<GenreGetQueryParams>, GenreGetQueryParamsValidator>();
builder.Services.AddScoped<IValidator<AuthorGetQueryParams>, AuthorGetQueryParamsValidator>();
builder.Services.AddScoped<IValidator<BookGetQueryParams>, BookGetQueryParamsValidator>();
builder.Services.AddControllers();
// Adds proper date only support for SwaggerUI
builder.Services.AddDateOnlyTimeOnlyStringConverters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Book catalog API",
        Description = "ASP.NET Core API that allows to manage books, along with the genres of the books and their authors."
    });
    options.AddSecurityDefinition("ApiKeyAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = AuthConstants.ApiKeyHeaderName,
        Description = "Global API key required to access the API.",
        Scheme = ""
    });
    //Enabled auth requirements globally
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKeyAuth" }
            },
            new [] { "readAccess", "writeAccess"}
        }
    });

    string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    const string xmlModels = "BookCatalogModels.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlModels));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyAuthMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();

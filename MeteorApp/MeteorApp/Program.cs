using FluentValidation;
using FluentValidation.AspNetCore;
using MeteorApp.DB;
using MeteorApp.DB.Predicate;
using MeteorApp.DB.Repository;
using MeteorApp.Interfaces;
using MeteorApp.Mappers;
using MeteorApp.Models;
using MeteorApp.Services;
using MeteorApp.Validation;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddControllers();

builder.Services.AddScoped<IMeteorsRecipientService, MeteorsRecipientService>();
builder.Services.AddScoped<IMeteorService, MeteorService>();
builder.Services.AddScoped<IRepository, MeteorRepository>();
builder.Services.AddScoped<MeteorContext>();
builder.Services.AddScoped<PredicateMeteorBuilder>();
builder.Services.AddScoped<IValidator<FilterMeteorsPaginationDTO>, FilterMeteorsValidator>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddMemoryCache();

await builder.Services.BuildServiceProvider()
    .GetService<IMeteorsRecipientService>()
    .GetMeteorsAndSaveInDBAsync();

var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
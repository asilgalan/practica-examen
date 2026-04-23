using Microsoft.AspNetCore.Mvc.Cors;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
using PracticandoExamenViernes.Data;
using PracticandoExamenViernes.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});


builder.Services.AddTransient<RepositoryComics>();
string connection = builder.Configuration.GetConnectionString("SqlAzure");
builder.Services.AddDbContext<ComicsContext>(options => options.UseSqlServer(connection));
var app = builder.Build();



app.MapOpenApi();
app.MapScalarApiReference();
app.MapGet("/", context =>
{
    context.Response.Redirect("/scalar");
    return Task.CompletedTask;
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

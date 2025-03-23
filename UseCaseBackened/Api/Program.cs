using Data;
using Domain.Repositries;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c=>{ 
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task API", Version = "v1" });
        c.EnableAnnotations(); }
);

builder.Services.AddSingleton<ITaskRepositry,TaskRepositry>(); // Register the manager

 builder.Services.AddCors(options =>
      {
          options.AddPolicy("AllowAll", builder =>
          {
              builder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();
          });
      });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.Run();



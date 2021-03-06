global using Microsoft.EntityFrameworkCore;
global using SCG.CAD.ETAX.DAL.EntityFramework;
global using SCG.CAD.ETAX.DAL;
global using SCG.CAD.ETAX.DAL.CONTROLLER;
global using SCG.CAD.ETAX.DAL.MODEL;
global using SCG.CAD.ETAX.MODEL;
global using Microsoft.AspNetCore.Mvc;
global using SCG.CAD.ETAX.API.Repositories;
global using SCG.CAD.ETAX.API.Services;
global using System.Data;
global using SCG.CAD.ETAX.MODEL.etaxModel;
global using System.Collections;
global using System.Globalization;
global using Newtonsoft.Json;



var builder = WebApplication.CreateBuilder(args);

var connectionString = new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["ConnectionStr"];

// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>
    (options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseSwagger();
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SCG.CAD.ETAX.API");
//    // c.RoutePrefix = "";
//});


//app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// app.UseResponseBuffering();

// app.UseResponseCaching();

app.Run();


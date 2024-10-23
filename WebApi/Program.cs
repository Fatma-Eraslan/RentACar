using Application;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Persistence;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddDistributedMemoryCache();//in memory (yay�nlad���m�z ortam�n belle�inde �al���yor.)

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//if(app.Environment.IsProduction())//hatay� detayl� �ekilde ver developmant i�in sistem bilgisi
app.ConfigureCustomExceptionMiddleware();//b�t�n hatalar� yapt���m�z middlewareden ge�sin

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

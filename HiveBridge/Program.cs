using Microsoft.AspNetCore.Authentication.JwtBearer;
using Project_HiveBridge.API;
using Project_HiveBridge.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCognitoIdentity();
builder.Services.AddAuthentication(options => AuthenticationHelper.Get(options)).AddJwtBearer(options => JwtBearerHelper.Get(options));
builder.Services.AddAuthorization(configure: AuthorizationHelper.Get);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

// Configure Rest APIs
app.ConfigureOpenDataApi();

app.Run();
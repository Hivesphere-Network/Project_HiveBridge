using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Project_HiveBridge.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCognitoIdentity();
Console.WriteLine(builder.Configuration["AWS"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = $"https://cognito-idp.ap-south-1.amazonaws.com/ap-south-1_rbjKdGeG7";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "https://cognito-idp.ap-south-1.amazonaws.com/ap-south-1_rbjKdGeG7",
        ValidateLifetime = true,
        LifetimeValidator = (before, expires, token, param) => expires > DateTime.UtcNow,
        ValidateAudience = false
    };
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

// Configure Rest APIs
app.ConfigureOpenDataApi();

app.Run();
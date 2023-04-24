using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Project_HiveBridge.API;
using Project_HiveBridge.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 5001, listenOptions =>
    {
        listenOptions.UseHttps("Properties/hivebridgeServerCert.pfx", "Deathcalls");
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
    });
});

builder.Services.AddGrpc();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddGrpcReflection();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://cognito-idp.ap-south-1.amazonaws.com/ap-south-1_rbjKdGeG7";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = "https://cognito-idp.ap-south-1.amazonaws.com/ap-south-1_rbjKdGeG7",
            IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
            {
                var json = new WebClient().DownloadString("https://cognito-idp.ap-south-1.amazonaws.com/ap-south-1_rbjKdGeG7/.well-known/jwks.json");
                var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(json);
                return keys.Keys;
            },
            ValidateIssuer = true,
            ValidateLifetime = true,
            LifetimeValidator = (_, expires, _, _) => expires > DateTime.UtcNow,
            ValidateAudience = false
        };
    });
 builder.Services.AddAuthorization(configure: options =>
 {
     options.AddPolicy("Admin", policy => policy.RequireClaim("cognito:groups", "Admin"));
     options.AddPolicy("User", policy => policy.RequireClaim("cognito:groups", "User"));
     options.AddPolicy("Any", policy => policy.RequireClaim("cognito:groups", "Admin", "User"));
 });


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure Rest APIs
app.ConfigureOpenDataApi();

app.MapGrpcReflectionService();
app.Run();
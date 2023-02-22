using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Project_HiveBridge.Helpers;

public abstract record JwtBearerHelper
{
    private static Action<JwtBearerOptions> Options => options =>
    {
        options.Authority = $"https://cognito-idp.ap-south-1.amazonaws.com/ap-south-1_rbjKdGeG7";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKeyResolver = (_, _, _, parameters) =>
            {
                string json = new WebClient().DownloadString(parameters.ValidIssuer + "/.well-known/jwks.json");
                var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(json)?.Keys;
                return keys!;
            },
            ValidateIssuer = true,
            ValidIssuer = "https://cognito-idp.ap-south-1.amazonaws.com/ap-south-1_rbjKdGeG7",
            ValidateLifetime = true,
            LifetimeValidator = (_, expires, _, _) => expires > DateTime.UtcNow,
            ValidateAudience = false
        };
    };
    public static Action<JwtBearerOptions> Get => Options;
    

}
using Microsoft.AspNetCore.Authorization;

namespace Project_HiveBridge.Helpers;

public abstract record AuthorizationHelper
{
    private static Action<AuthorizationOptions> Options => options =>
    {
        options.AddPolicy("Admin", policy => policy.RequireClaim("cognito:groups", "Admin"));
        options.AddPolicy("User", policy => policy.RequireClaim("cognito:groups", "User"));
        options.AddPolicy("Any", policy => policy.RequireClaim("cognito:groups", "Admin", "User"));
    };
    public static Action<AuthorizationOptions> Get => Options;
}
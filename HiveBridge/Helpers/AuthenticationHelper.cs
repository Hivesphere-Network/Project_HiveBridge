using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Project_HiveBridge.Helpers;

public abstract record AuthenticationHelper
{
    private static Action<AuthenticationOptions> Options => options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    };

    public static Action<AuthenticationOptions> Get => Options;
}
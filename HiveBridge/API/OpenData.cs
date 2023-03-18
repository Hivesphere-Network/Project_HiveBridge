namespace Project_HiveBridge.API;

public static class OpenData
{
    public static void ConfigureOpenDataApi(this WebApplication app)
    {
        app.MapGet("/api/TestConnection", TestConnectionAsync).WithName("TestConnection");
        app.MapGet("/api/TestAuthentication", TestAuthenticationAsync).WithName("TestAuthentication").RequireAuthorization();
    }

    private static Task<IResult> TestConnectionAsync()
    {
        return Task.FromResult(Results.Ok("Connected"));
    }

    private static Task<IResult> TestAuthenticationAsync()
    {
        return Task.FromResult(Results.Ok("Authenticated"));
    }

}
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
        return Task.FromResult(Results.Ok());
    }
    
    private static Task<IResult> TestAuthenticationAsync()
    {
        Results.Text("Authenticated");
        return Task.FromResult(Results.Ok());
    }

}
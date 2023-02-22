namespace Project_HiveBridge.API;

public static class OpenData
{
    public static void ConfigureOpenDataApi(this WebApplication app)
    {
        app.MapGet("/api/TestConnection", TestConnection).WithName("TestConnection");
        app.MapGet("/api/TestAuthentication", TestAuthentication).WithName("TestAuthentication").RequireAuthorization();
    }

    private static Task<IResult> TestConnection()
    {
        return Task.FromResult(Results.Ok());
    }

    private static Task<IResult> TestAuthentication()
    {
        Results.Text("Authenticated");
        return Task.FromResult(Results.Ok());
    }

}
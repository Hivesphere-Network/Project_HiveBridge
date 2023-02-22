namespace Project_HiveBridge.API;

public static class OpenData
{
    public static void ConfigureOpenDataApi(this WebApplication app)
    {
        app.MapGet("/api/TestConnection", TestConnection).WithName("TestConnection");
    }

    private static Task<IResult> TestConnection()
    {
        return Task.FromResult(Results.Ok());
    }

}
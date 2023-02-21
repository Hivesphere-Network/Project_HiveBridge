namespace Project_HiveBridge.API;

public class OpenData
{
    public OpenData(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; set; }
    
    public void ConfigureOpenData(IConfiguration configuration)
    {
        Configuration = configuration;
    }
}
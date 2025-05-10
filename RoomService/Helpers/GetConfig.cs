
namespace RoomService.Helpers
{
    public class GetConfig
    {
        public static IConfiguration AppSetting { get; }
        static GetConfig()
        {
            AppSetting = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory) 
            .AddJsonFile("Room.appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        }
    }
}

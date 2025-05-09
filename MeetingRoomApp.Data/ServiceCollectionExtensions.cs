using MeetingRoomApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeetingRoomApp.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDataServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<dbContext>(o => 
            o.UseSqlServer(configuration.GetConnectionString("ApiContext")));
            return services;
        }
    }
}

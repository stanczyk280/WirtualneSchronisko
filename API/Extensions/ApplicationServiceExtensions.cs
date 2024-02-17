using API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Reflection.Metadata.Ecma335;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped(typeof(IAnimalRepository), typeof(AnimalRepository));
            services.AddScoped(typeof(IShelterRepository), typeof(ShelterRepository));
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(opt =>
            {
                opt.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<DataContext>();

            return services;
        }
    }
}
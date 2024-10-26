using Microsoft.EntityFrameworkCore;
using MvcExercise.Helper;
using MvcExercise.Interfaces;
using MvcExercise.Models;
using MvcExercise.Repo;
using MvcExercise.Services;

namespace MvcExercise.Extension
{
    public static class AddServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<MvcExerciseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MvcExerciseContext")));
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUserService, UserSerive>();
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            return services;
        }
    }
}

using Application.Services;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure;
using Infrastructure.UsuarioRepository;
using Microsoft.EntityFrameworkCore;

namespace API.IoC
{
    public static class ServicesExtension
    {
        public static void AddServicesSdk(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UsuarioDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioService, UsuarioService>();

        }
    }
}
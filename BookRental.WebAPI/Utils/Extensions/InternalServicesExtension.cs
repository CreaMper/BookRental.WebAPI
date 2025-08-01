using BookRental.EF;
using BookRental.WebAPI.Converters;
using BookRental.WebAPI.Converters.Interfaces;
using BookRental.WebAPI.Utils.Validators;
using BookRental.WebAPI.Utils.Validators.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookRental.WebAPI.Utils.Extensions
{
    public static class InternalServicesExtension
    {
        public static IServiceCollection AddInternalServices(this IServiceCollection services)
        {
            services.AddScoped<IFactory, Factory>();
            services.AddScoped<IBookStatusValidator, BookStatusValidator>();

            services.AddTransient<IBookConverter, BookConverter>();
            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IWebHostEnvironment _env)
        {
            var connectionString = _env.IsDevelopment()
                ? Environment.GetEnvironmentVariable("BR_ConnectionString_DEV")
                : Environment.GetEnvironmentVariable("BR_ConnectionString_PROD");

            if (string.IsNullOrEmpty(connectionString))
                throw new NullReferenceException("Cannot fetch connection string!");

            services.AddDbContext<MainDbContext>(options =>
                options.UseSqlServer(connectionString)
            );

            return services;
        }
    }
}

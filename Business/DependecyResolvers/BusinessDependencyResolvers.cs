using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.Couchbase;
using DataAccess.Concrete.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace Business.DependecyResolvers
{
    public static class BusinessDependencyResolvers
    {
        public static IServiceCollection AddDependencyBusinessResolvers(this IServiceCollection services)
        {
            //JWT
            services.AddScoped<ITokenHelper, JwtHelper>();

            services.AddScoped<IAppUserService, AppUserManager>();
            services.AddScoped<IAppUserDal, AppUserDal>();

            services.AddScoped<IAuthService, AuthManager>();

            services.AddScoped<ITodoService, TodoManager>();
            services.AddScoped<ITodoDal, TodoDal>();

            return services;
        }
    }
}

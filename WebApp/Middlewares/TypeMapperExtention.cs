using Microsoft.AspNetCore.Builder;

namespace WebApp.Middlewares
{
    public static class TypeMapperExtention
    {
        public static IApplicationBuilder UseTrpeMapper(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TypeMapper>();
        }
    }
}

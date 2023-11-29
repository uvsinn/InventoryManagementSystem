using CommonHttpContext = CommonServices.HttpContext;

namespace CommonServices
{
    public static class AppExtensionsS
    {
        public static void Configure(this IApplicationBuilder app)
        {
            CommonHttpContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
        }
    }
}
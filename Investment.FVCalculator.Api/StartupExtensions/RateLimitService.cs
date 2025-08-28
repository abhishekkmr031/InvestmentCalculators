using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace Investment.FVCalculator.Api.StartupExtensions
{
    public static class RateLimitService
    {
        public static void RateLimiter(this IServiceCollection services)
        {
            services.AddRateLimiter(options => {

                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddPolicy("fixedMyPolicy", httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 2,
                        Window = TimeSpan.FromSeconds(100)

                    }));

            });
        }
    }
}

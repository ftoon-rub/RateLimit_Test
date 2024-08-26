using AspNetCoreRateLimit;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
namespace RateLimit_Test.Dependancy
{
    public static class DependancyInjectionRegistration
    {
        public static IServiceCollection AddIpRateLimitServiceInjection(this IServiceCollection services,IConfiguration configuration)
        {
            // Configure IP rate limiting with MemoryCahcing

            //services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
            //services.AddMemoryCache();
            //services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            //services.AddInMemoryRateLimiting();
            //services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            //services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();




            // Configure IP rate limiting with Redis
            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
           services.AddSingleton<IIpPolicyStore, DistributedCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, DistributedCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>(); // Example registration

            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>(); 
            services.AddDistributedRateLimiting<AsyncKeyLockProcessingStrategy>();
            return services;
            
        }
    }
}

using AspNetCoreRateLimit;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;

namespace RateLimit_Test.Dependancy
{
    //public class DistributedCacheIpPolicyStore : IIpPolicyStore
    //{
    //    private readonly IDistributedCache _distributedCache;
    //    public DistributedCacheIpPolicyStore(IDistributedCache distributedCache)
    //    {
    //        _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
    //    }

    //    public async Task<bool> ExistsAsync(string clientId, CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        try
    //        {
    //            var policiesJson = await _distributedCache.GetStringAsync(GetCacheKey(clientId), cancellationToken);
    //            return policiesJson != null;
    //        }
    //        catch (Exception ex)
    //        {
    //            // Log or handle the exception as needed
    //            throw new ApplicationException($"Failed to check existence of IP policies for clientId '{clientId}'", ex);
    //        }
    //    }

    //    public async Task<IpRateLimitPolicies> GetAsync(string clientId, CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        var policiesJson = await _distributedCache.GetStringAsync(GetCacheKey(clientId), cancellationToken);
    //        return policiesJson != null ? JsonConvert.DeserializeObject<IpRateLimitPolicies>(policiesJson) : null;
    //    }

    //    public async Task RemoveAsync(string clientId, CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        await _distributedCache.RemoveAsync(GetCacheKey(clientId), cancellationToken);
    //    }

    //    public Task SeedAsync()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public async Task SetAsync(string clientId, IpRateLimitPolicies policies, CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        var policiesJson = JsonConvert.SerializeObject(policies);
    //        var options = new DistributedCacheEntryOptions
    //        {
    //            SlidingExpiration = TimeSpan.FromMinutes(10) // Example expiration time
    //        };
    //        await _distributedCache.SetStringAsync(GetCacheKey(clientId), policiesJson, options, cancellationToken);
    //    }

    //    public Task SetAsync(string id, IpRateLimitPolicies entry, TimeSpan? expirationTime = null, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    private string GetCacheKey(string clientId)
    //    {
    //        return $"ip_policies_{clientId}";
    //    }
    //}





    //public class DistributedCacheRateLimitCounterStore : IRateLimitCounterStore
    //{
    //    private readonly IDistributedCache _distributedCache;

    //    public DistributedCacheRateLimitCounterStore(IDistributedCache distributedCache)
    //    {
    //        _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
    //    }

    //    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        var counter = await GetAsync(id, cancellationToken);
    //        return counter!=null;
    //    }
    //    public async Task<RateLimitCounter?> GetAsync(string id, CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        var counterJson = await _distributedCache.GetStringAsync(id, cancellationToken);
    //        return counterJson != null ? JsonConvert.DeserializeObject<RateLimitCounter>(counterJson) : null;
    //    }
    //    public async Task RemoveAsync(string id, CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        await _distributedCache.RemoveAsync(id, cancellationToken);
    //    }
    //    public async Task SetAsync(string id, RateLimitCounter counter, TimeSpan expirationTime, CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        var counterJson = JsonConvert.SerializeObject(counter);
    //        var options = new DistributedCacheEntryOptions
    //        {
    //            AbsoluteExpirationRelativeToNow = expirationTime
    //        };
    //        await _distributedCache.SetStringAsync(id, counterJson, options, cancellationToken);
    //    }
    //    public async  Task SetAsync(string id, RateLimitCounter? counter, TimeSpan? expirationTime = null, CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        var counterJson = JsonConvert.SerializeObject(counter);
    //        var options = new DistributedCacheEntryOptions
    //        {
    //            AbsoluteExpirationRelativeToNow = expirationTime
    //        };
    //        await _distributedCache.SetStringAsync(id, counterJson, options, cancellationToken);
    //    }
    //    async Task<RateLimitCounter?> IRateLimitStore<RateLimitCounter?>.GetAsync(string id, CancellationToken cancellationToken=default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        var counterJson = await _distributedCache.GetStringAsync(id, cancellationToken);
    //        return counterJson != null ? JsonConvert.DeserializeObject<RateLimitCounter>(counterJson) : null;
    //    }
    //}

//    public class AsyncKeyLockProcessingStrategy : IProcessingStrategy
//    {
//        private readonly IRateLimitCounterStore _counterStore;
//        private readonly IIpPolicyStore _policyStore;

//        public AsyncKeyLockProcessingStrategy(IRateLimitCounterStore counterStore, IIpPolicyStore policyStore)
//        {
//            _counterStore = counterStore ?? throw new ArgumentNullException(nameof(counterStore));
//            _policyStore = policyStore ?? throw new ArgumentNullException(nameof(policyStore));
//        }

//        public async  Task<RateLimitCounter> ProcessRequestAsync(ClientRequestIdentity requestIdentity, RateLimitRule rule, ICounterKeyBuilder counterKeyBuilder, RateLimitOptions rateLimitOptions, CancellationToken cancellationToken = default)
//        {
           
//                var counter = await _counterStore.GetAsync($"{requestIdentity.ClientId}_{rule.Endpoint}"); // Example key format
//                if (counter == null)
//                {
//                    counter = new RateLimitCounter
//                    {
//                        Timestamp = DateTime.UtcNow,
//                        Count = 0
//                    };
//                }

//                // Check if the client has exceeded the rate limit
//                var result = counter.Count >= rule.Limit;

//                return result;
            

  
//        }
//        public async Task<string> ComputeCounterKeyAsync(ClientRequestIdentity requestIdentity, RateLimitRule rule)
//        {
//            // Compute a unique key based on the request identity and rate limit rule
//            // Example: $"{requestIdentity.ClientId}_{rule.Endpoint}"
//            var key = $"{requestIdentity.ClientId}_{rule.Endpoint}";
//            return key;
//        }

//        public async Task<RateLimitCounter> ProcessRequestAsync(ClientRequestIdentity requestIdentity, RateLimitRule rule)
//        {
//            var counterKey = await ComputeCounterKeyAsync(requestIdentity, rule);
//            var counter = await _counterStore.GetAsync(counterKey);

//            if (counter == null)
//            {
//                counter = new RateLimitCounter
//                {
//                    Timestamp = DateTime.UtcNow,
//                    Count = 0
//                };
//            }

//            // Update the counter
//            counter.Count++;
//            await _counterStore.SetAsync(counterKey, counter, rule.Period);

//            return counter;
//        }
//    ////}
}

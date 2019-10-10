using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace StardewModdingAPI.Web.Framework.Caching
{
    /// <summary>Reads and writes cache data to a Redis database.</summary>
    internal class RedisCache : ICache, IDisposable
    {
        /*********
        ** Fields
        *********/
        /// <summary>The Redis database in which to cache data.</summary>
        private readonly IDatabase Redis;

        /// <summary>A string to prefix to all cache keys.</summary>
        private readonly string KeyPrefix;


        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="connectionString">The Redis connection string.</param>
        /// <param name="keyPrefix">A string to prefix to all cache keys.</param>
        public RedisCache(string connectionString, string keyPrefix)
        {
            this.Redis = ConnectionMultiplexer.Connect(connectionString).GetDatabase();
            this.KeyPrefix = keyPrefix;
        }

        /// <summary>Get a data model stored in the cache.</summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="key">The model's unique key in the cache.</param>
        public T Get<T>(string key)
        {
            key = this.GetCacheKey(key);

            RedisValue value = this.Redis.StringGet(key);
            return value.HasValue
                ? JsonConvert.DeserializeObject<T>(value.ToString())
                : default;
        }

        /// <summary>Save a data model to the cache.</summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="key">The model's unique key in the cache.</param>
        /// <param name="value">The model to store.</param>
        /// <param name="expiry">The amount of time the entry should be kept in the cache.</param>
        public void Set<T>(string key, T value, TimeSpan? expiry = null)
        {
            key = this.GetCacheKey(key);

            string raw = JsonConvert.SerializeObject(value);
            this.Redis.StringSet(key, raw, expiry);
        }

        /// <summary>Delete an entry from the cache.</summary>
        /// <param name="key">The key to delete.</param>
        public void Delete(string key)
        {
            key = this.GetCacheKey(key);
            this.Redis.KeyDelete(key);
        }

        /// <summary>Release all resources.</summary>
        public void Dispose()
        {
            this.Redis.Multiplexer.Dispose();
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Get all keys matching a key prefix on the primary database server.</summary>
        /// <param name="prefix">The key prefix to match.</param>
        private IEnumerable<RedisKey> GetKeys(string prefix)
        {
            prefix = this.GetCacheKey(prefix);

            EndPoint endpoint = this.Redis.Multiplexer.GetEndPoints().First();
            IServer server = this.Redis.Multiplexer.GetServer(endpoint);
            return server.Keys(pattern: $"{prefix}*");
        }

        /// <summary>Get a normalised cache key with the prefix attached.</summary>
        /// <param name="key">The key to normalise.</param>
        private string GetCacheKey(string key)
        {
            return !string.IsNullOrWhiteSpace(this.KeyPrefix)
                ? $"{this.KeyPrefix}{key}"
                : key;
        }
    }
}

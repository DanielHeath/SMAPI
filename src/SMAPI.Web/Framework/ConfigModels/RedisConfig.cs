namespace StardewModdingAPI.Web.Framework.ConfigModels
{
    /// <summary>The config settings for the Redis cache database.</summary>
    internal class RedisConfig
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The Redis connection string.</summary>
        public string ConnectionString { get; set; }

        /// <summary>The prefix to use for all cache keys.</summary>
        public string KeyPrefix { get; set; }
    }
}

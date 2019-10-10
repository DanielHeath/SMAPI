using System;

namespace StardewModdingAPI.Web.Framework.Caching
{
    /// <summary>Reads and writes cache data.</summary>
    internal interface ICache
    {
        /*********
        ** Public methods
        *********/
        /// <summary>Get a data model stored in the cache.</summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="key">The model's unique key in the cache.</param>
        T Get<T>(string key);

        /// <summary>Save a data model to the cache.</summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="key">The model's unique key in the cache.</param>
        /// <param name="value">The model to store.</param>
        /// <param name="expiry">The amount of time the entry should be kept in the cache.</param>
        void Set<T>(string key, T value, TimeSpan? expiry = null);

        /// <summary>Delete an entry from the cache.</summary>
        /// <param name="key">The key to delete.</param>
        void Delete(string key);
    }
}

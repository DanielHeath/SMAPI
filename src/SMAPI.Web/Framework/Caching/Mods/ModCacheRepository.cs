using System;
using StardewModdingAPI.Toolkit.Framework.UpdateData;
using StardewModdingAPI.Web.Framework.ModRepositories;

namespace StardewModdingAPI.Web.Framework.Caching.Mods
{
    /// <summary>Encapsulates logic for accessing the mod data cache.</summary>
    internal class ModCacheRepository : BaseCacheRepository, IModCacheRepository
    {
        /*********
        ** Fields
        *********/
        /// <summary>The cache in which to store data.</summary>
        private readonly ICache Cache;

        /// <summary>The maximum time to cache mods.</summary>
        private readonly TimeSpan MaxCacheTime;


        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="cache">The cache in which to store data.</param>
        /// <param name="maxCacheTime">The maximum time to cache mods.</param>
        public ModCacheRepository(ICache cache, TimeSpan maxCacheTime)
        {
            this.Cache = cache;
            this.MaxCacheTime = maxCacheTime;
        }

        /// <summary>Get the cached mod data.</summary>
        /// <param name="site">The mod site to search.</param>
        /// <param name="id">The mod's unique ID within the <paramref name="site"/>.</param>
        /// <param name="mod">The fetched mod.</param>
        /// <param name="markRequested">Whether to update the mod's 'last requested' date.</param>
        public bool TryGetMod(ModRepositoryKey site, string id, out CachedMod mod, bool markRequested = true)
        {
            // get mod
            string cacheKey = this.GetCacheKey(site, id);
            mod = this.Cache.Get<CachedMod>(cacheKey);
            if (mod == null)
                return false;

            // bump 'last requested'
            if (markRequested)
            {
                mod.LastRequested = DateTimeOffset.UtcNow;
                mod = this.SaveMod(mod);
            }

            return true;
        }

        /// <summary>Save data fetched for a mod.</summary>
        /// <param name="site">The mod site on which the mod is found.</param>
        /// <param name="id">The mod's unique ID within the <paramref name="site"/>.</param>
        /// <param name="mod">The mod data.</param>
        /// <param name="cachedMod">The stored mod record.</param>
        public void SaveMod(ModRepositoryKey site, string id, ModInfoModel mod, out CachedMod cachedMod)
        {
            id = this.NormalizeId(id);
            cachedMod = this.SaveMod(new CachedMod(site, id, mod));
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Save data fetched for a mod.</summary>
        /// <param name="mod">The mod data.</param>
        public CachedMod SaveMod(CachedMod mod)
        {
            string cacheKey = this.GetCacheKey(mod.Site, mod.ID);
            this.Cache.Set(cacheKey, mod, this.MaxCacheTime);

            return mod;
        }

        /// <summary>Normalize a mod ID for case-insensitive search.</summary>
        /// <param name="id">The mod ID.</param>
        public string NormalizeId(string id)
        {
            return id.Trim().ToLower();
        }

        /// <summary>Get the cache key for a mod entry.</summary>
        /// <param name="site">The mod site on which the mod is found.</param>
        /// <param name="id">The mod's unique ID within the <paramref name="site"/>.</param>
        public string GetCacheKey(ModRepositoryKey site, string id)
        {
            return $"mods.{site}.{this.NormalizeId(id)}";
        }
    }
}

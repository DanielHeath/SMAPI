using System.Collections.Generic;
using System.Linq;
using StardewModdingAPI.Toolkit.Framework.Clients.Wiki;

namespace StardewModdingAPI.Web.Framework.Caching.Wiki
{
    /// <summary>Encapsulates logic for accessing the wiki data cache.</summary>
    internal class WikiCacheRepository : BaseCacheRepository, IWikiCacheRepository
    {
        /*********
        ** Fields
        *********/
        /// <summary>The cache in which to store data.</summary>
        private readonly ICache Cache;


        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="cache">The cache in which to store data.</param>
        public WikiCacheRepository(ICache cache)
        {
            this.Cache = cache;
        }

        /// <summary>Get the cached wiki metadata.</summary>
        /// <param name="metadata">The fetched metadata.</param>
        public bool TryGetWikiMetadata(out CachedWikiMetadata metadata)
        {
            metadata = this.Cache.Get<CachedWikiMetadata>("wiki-metadata");
            return metadata != null;
        }

        /// <summary>Get the cached wiki mods.</summary>
        public IEnumerable<CachedWikiMod> GetWikiMods()
        {
            return this.Cache.GetAll<CachedWikiMod>(prefix: "wiki-mods.");
        }

        /// <summary>Save data fetched from the wiki compatibility list.</summary>
        /// <param name="stableVersion">The current stable Stardew Valley version.</param>
        /// <param name="betaVersion">The current beta Stardew Valley version.</param>
        /// <param name="mods">The mod data.</param>
        /// <param name="cachedMetadata">The stored metadata record.</param>
        /// <param name="cachedMods">The stored mod records.</param>
        public void SaveWikiData(string stableVersion, string betaVersion, IEnumerable<WikiModEntry> mods, out CachedWikiMetadata cachedMetadata, out CachedWikiMod[] cachedMods)
        {
            cachedMetadata = new CachedWikiMetadata(stableVersion, betaVersion);
            cachedMods = mods.Select(mod => new CachedWikiMod(mod)).ToArray();

            this.Cache.Delete("wiki-metadata");
            this.Cache.DeleteAll("wiki-mods.");

            this.Cache.Set("wiki-metadata", cachedMetadata);
            for (int i = 0; i < cachedMods.Length; i++)
                this.Cache.Set($"wiki-mods.{i}", cachedMods[i]);
        }
    }
}

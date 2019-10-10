using System;

namespace StardewModdingAPI.Web.Framework.Caching.Wiki
{
    /// <summary>The model for cached wiki metadata.</summary>
    internal class CachedWikiMetadata
    {
        /*********
        ** Accessors
        *********/
        /// <summary>When the data was last updated.</summary>
        public DateTimeOffset LastUpdated { get; set; }

        /// <summary>The current stable Stardew Valley version.</summary>
        public string StableVersion { get; set; }

        /// <summary>The current beta Stardew Valley version.</summary>
        public string BetaVersion { get; set; }


        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        public CachedWikiMetadata() { }

        /// <summary>Construct an instance.</summary>
        /// <param name="stableVersion">The current stable Stardew Valley version.</param>
        /// <param name="betaVersion">The current beta Stardew Valley version.</param>
        public CachedWikiMetadata(string stableVersion, string betaVersion)
        {
            this.StableVersion = stableVersion;
            this.BetaVersion = betaVersion;
            this.LastUpdated = DateTimeOffset.UtcNow;
        }
    }
}

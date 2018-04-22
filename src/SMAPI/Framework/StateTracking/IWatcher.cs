using System;

namespace StardewModdingAPI.Framework.StateTracking
{
    /// <summary>A watcher which detects changes to something.</summary>
    internal interface IWatcher : IDisposable
    {
        /*********
        ** Accessors
        *********/
        /// <summary>Whether the value changed since the last reset.</summary>
        bool IsChanged { get; }


        /*********
        ** Methods
        *********/
        /// <summary>Update the current value if needed.</summary>
        void Update();

        /// <summary>Set the current value as the baseline.</summary>
        void Reset();
    }
}

## NOTICE: THIS PROJECT IS STILL IN ALPHA

# What is SMAPI

SMAPI (Stardew Mapping Application Programming Interface) is a tool to help modders make changes to Stardew. It is a standalone executable which goes alongside your Stardew.exe.

## Latest Version: 0.37.1
- Minor update to avoid confusion by me forgetting to update the version information. Altered how version info is stored.
- Logging is now in it's own contained class and no longer part of the main program. Deprecated functions still exist to keep a bit of backwards compatibility but will eventually be removed.
- For Modders: Logging has changed, other than that nothing you need to worry about from 0.37.0
Tested and works with current versions of: SmartMod, StardewCJB , FreezeInside

Download: https://github.com/ClxS/SMAPI/releases/tag/0.37.1

## Installation

To install SMAPI:
- Firstly, make sure you have .NET 4.5. You can get it here: https://www.microsoft.com/en-gb/download/details.aspx?id=30653
- Download the the latest release binary here: https://github.com/ClxS/SMAPI/releases/latest
- Extract the zip file alongside your Stardew.exe, for example, if using Steam this would be somewhere like C:/ProgramFiles/Steam/steamapps/common/StardewValley
- To launch SMAPI, launch StardewValleyModdingAPI.exe

To install mods:
- To install mods just download the mod's .DLL file, and place it in %appdata%\StardewValley\Mods\. SMAPI will take care of the rest!

## Future Plans
- Content only mods
- Support for a wide range of events
- Enable the addition of new custom content such as locations, NPCs, and items.

## Mod Developers!

Mod developers would work off the release branch. The master branch will contain mid-version updates which could make your mods incompatable with both the current release and the upcoming releases.
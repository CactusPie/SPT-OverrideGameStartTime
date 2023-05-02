using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using JetBrains.Annotations;
using TarkovDateTime = GClass1252;

namespace CactusPie.OverrideGameStartTime
{
    [BepInPlugin("com.cactuspie.overridetime", "CactusPie.OverrideGameStartTime", "1.0.0")]
    public class OverrideTimePlugin : BaseUnityPlugin
    {
        internal static ConfigEntry<int> Hour { get; set; }
        internal static ConfigEntry<int> Minutes { get; set; }
        internal static ConfigEntry<bool> OverrideStartTime { get; set; }

        [UsedImplicitly]
        internal void Start()
        {
            const string sectionName = "Game start time settings";
            
            OverrideStartTime = Config.Bind
            (
                sectionName,
                "Override game start time",
                false,
                new ConfigDescription
                (
                    "Whether the game start time should be modified"
                )
            );
            
            Hour = Config.Bind
            (
                sectionName,
                "Game Hour",
                0,
                new ConfigDescription
                (
                    "Game Hour", 
                    new AcceptableValueRange<int>(0, 23)
                )
            );
            
            Minutes = Config.Bind
            (
                sectionName,
                "Game Minutes",
                0,
                new ConfigDescription
                (
                    "Game minutes", 
                    new AcceptableValueRange<int>(0, 59)
                )
            );

            new OverrideTimePatch().Enable();
        }
    }
}

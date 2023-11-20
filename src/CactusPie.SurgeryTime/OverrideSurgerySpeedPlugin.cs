using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using JetBrains.Annotations;

namespace CactusPie.SurgeryTime
{
    [BepInPlugin("com.cactuspie.surgerytime", "CactusPie.SurgeryTime", "1.0.0")]
    public class OverrideSurgerySpeedPlugin : BaseUnityPlugin
    {
        internal static ConfigEntry<float> SurgerySpeedMultiplier { get; set; }
        internal static ConfigEntry<bool> SurgeryTimeOverrideEnabled { get; set; }

        [UsedImplicitly]
        internal void Start()
        {
            const string sectionName = "Surgery time settings";
            
            SurgeryTimeOverrideEnabled = Config.Bind
            (
                sectionName,
                "Enable surgery speed multiplier",
                true,
                new ConfigDescription
                (
                    "Whether or not the surgery speed multiplier is enabled"
                )
            );
            
            SurgerySpeedMultiplier = Config.Bind
            (
                sectionName,
                "Surgery speed multiplier",
                2.0f,
                new ConfigDescription
                (
                    "Values above 1.0 will make the surgery faster, lower values will make it slower", 
                    new AcceptableValueRange<float>(0.1f, 100.0f)
                )
            );

            new OverrideSurgerySpeedPatch().Enable();
        }
        
        [UsedImplicitly]
        public void OnDestroy()
        {
            Destroy(this);
        }
    }
}

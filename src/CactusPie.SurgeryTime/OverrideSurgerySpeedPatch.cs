using System;
using System.Reflection;
using Aki.Reflection.Patching;
using EFT.InventoryLogic;

namespace CactusPie.SurgeryTime
{
    public class OverrideSurgerySpeedPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            MethodInfo method = typeof(HealthEffectsComponent).GetMethod("UseTimeFor", BindingFlags.Public | BindingFlags.Instance);
            return method;
        }

        [PatchPostfix]
        public static void PatchPostfix(ref float __result, HealthEffectsComponent __instance, EBodyPart bodyPart)
        {
            if (__instance == null)
            {
                return;
            }
            
            if (!OverrideSurgerySpeedPlugin.SurgeryTimeOverrideEnabled.Value)
            {
                return;
            }
            
            string itemId = __instance.Item.Template.Name;
            const string surv12Id = "survival_first_aid_rollup_kit";
            const string cmsId = "core_medical_surgical_kit";

            if (itemId == cmsId || itemId == surv12Id)
            {
                __result /= OverrideSurgerySpeedPlugin.SurgerySpeedMultiplier.Value;
            }
        }
    }
}
using System;
using System.Reflection;
using Aki.Reflection.Patching;
using Comfort.Common;
using EFT;
using EFT.InventoryLogic;

namespace CactusPie.OverrideGameStartTime
{
    public class OverrideTimePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            // Executes on game start
            Type type = typeof(GameWorld).Assembly.GetType("EFT.LocalGame");
            MethodInfo method = type.GetMethod("smethod_5", BindingFlags.Static | BindingFlags.NonPublic);
            
            return method;
        }

        [PatchPostfix]
        public static void PatchPostfix()
        {
            if (!OverrideTimePlugin.OverrideStartTime.Value)
            {
                return;
            }

            DateTime now = DateTime.Now;
            var gameDateTime = new DateTime(now.Year, now.Month, now.Day, OverrideTimePlugin.Hour.Value, OverrideTimePlugin.Minutes.Value, 0);
            
            Singleton<GameWorld>.Instance.GameDateTime.Reset(gameDateTime);
        }
    }
}
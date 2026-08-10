using HarmonyLib;
using MGSC;

namespace QM_RecycleHotKey.Patches.NoWeaponAmputation
{
    /// <summary>
    /// Changes the amputation weapon to not lose durability when the AmputateWithoutWeapon option is true.
    /// </summary>
    [HarmonyPatch(typeof(BreakableItemComponent), nameof(BreakableItemComponent.Break))]
    public static class BreakableItemComponent_Break_Patch
    {
        public static bool Prepare()
        {
            return Plugin.Config.AmputateWithoutWeapon;
        }

        public static bool Prefix()
        {
            return !CorpseInspectWindow_AmputateSlot_Patch.IsAmputationRunning;
        }
    }
}

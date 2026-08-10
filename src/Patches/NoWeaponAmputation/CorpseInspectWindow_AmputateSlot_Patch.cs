using HarmonyLib;
using MGSC;

namespace QM_RecycleHotKey.Patches.NoWeaponAmputation
{
    /// <summary>
    /// Sets the flag to the weapon "break" that durability should not be reduced when 
    /// AmputateWithoutWeapon is true.
    /// </summary>
    [HarmonyPatch(typeof(CorpseInspectWindow), nameof(CorpseInspectWindow.AmputateSlot))]
    public static class CorpseInspectWindow_AmputateSlot_Patch
    {

        /// <summary>
        /// True if the amputation is running with the AmputateWithoutWeapon option.
        /// </summary>
        public static bool IsAmputationRunning { get; private set; } = false;

        public static bool Prepare()
        {
            return Plugin.Config.AmputateWithoutWeapon;
        }


        public static void Prefix()
        {
            //No weapon amputation is running
            IsAmputationRunning = true;
        }

        public static void Postfix()
        {
            //No weapon amputation is done
            IsAmputationRunning = false;
        }   
    }
}

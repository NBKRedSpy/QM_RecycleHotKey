using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MGSC.CorpseInventoryView;

namespace QM_RecycleHotKey.NoWeaponAmputation
{

    [HarmonyPatch(typeof(CorpseInventoryView), nameof(CorpseInventoryView.CanAmputate))]
    internal static class CorpseInventoryView_CanAmputate_Patch
    {
        public static bool Prepare()
        {
            return Plugin.Config.AmputateWithoutWeapon;
        }

        public static bool Prefix(CorpseInventoryView __instance, ref bool __result)
        {
            //copy of the original.

            //---- Original Code start
            //bool flag = _creatures.Player.Inventory.GetFirstAmputationWeapon() != null;
            //return _workMode == WorkMode.ShowCorpse && flag;
            //---- Original Code End

            //Remove the amputation weapon check.
            __result = __instance._workMode == WorkMode.ShowCorpse;

            return false;
        }
    }
}

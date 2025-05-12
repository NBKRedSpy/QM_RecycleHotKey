using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MGSC;

namespace QM_RecycleHotKey
{
    [HarmonyPatch(typeof(InventoryScreen), nameof(InventoryScreen.DragControllerRefreshCallback))]
    public static class InventoryScreen_TakeAllFromCorpse_Patch
    {
        public static bool Prepare()
        {
            return !Plugin.Config.DoNoCloseWindowOnEmpty;
        }
        public static void Postfix(InventoryScreen __instance)
        {
            //Set the flag that handles the auto close.
            //Normally the game ignores new tabs that are created via dropping items.

            //This function is called before the tab check occurs.
            __instance._hideAfterItemsOnFloorLooted = __instance._tabsView.TabsCount == 1;
        }
    }
}

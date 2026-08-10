using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MGSC;

namespace QM_RecycleHotKey.Patches
{
    [HarmonyPatch(typeof(InventoryScreen), nameof(InventoryScreen.DragControllerRefreshCallback))]
    public static class InventoryScreen_DragControllerRefreshCallback_Patch
    {
        public static bool Prepare()
        {
            return !Plugin.Config.DoNoCloseWindowOnEmpty;
        }
        public static void Postfix(InventoryScreen __instance)
        {
            //  This is a hack to avoid using a transpiler.
            //   If the only tab is the empty corpse, then do not auto close the window.
            //  the next line in the code checks this flag *and* if the corpse is empty.
            __instance._hideAfterItemsOnFloorLooted = __instance._tabsView.TabsCount == 1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MGSC;

namespace QM_RecycleHotKey.Patches
{
    /// <summary>
    /// Adds the logic to not close an empty corpse window or tab if there is more than one tab.
    /// </summary>
    [HarmonyPatch(typeof(InventoryScreen), nameof(InventoryScreen.DragControllerRefreshCallback))]
    public static class InventoryScreen_DragControllerRefreshCallback_Patch
    {

        public static void Postfix(InventoryScreen __instance)
        {
            if (!Plugin.Config.DoNoCloseWindowOnEmpty) return;

            //  Patch note - This is a hack to avoid using a transpiler.
            //  the next line in the code checks this flag *and* if the corpse is empty.

            //  If there is more than one tab, do not close auto close on take all,
            //  even if the corpse or current tab is completely empty.
            //  The vanilla game will close the window even if a recycle command drops new items.

            //Patch notes

            //   If the only tab an empty corpse or tab, then do not auto close the window.
            //  the next line in the code checks this flag *and* if the tab is a corpse, it is completely empty.
            __instance._hideAfterItemsOnFloorLooted = __instance._tabsView.TabsCount == 1;
        }
    }
}

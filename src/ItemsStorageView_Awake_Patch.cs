using HarmonyLib;
using MGSC;
using QM_RecycleHotKey.StorageRecyclePatches;

namespace QM_RecycleHotKey
{
    [HarmonyPatch(typeof(InventoryScreen), nameof(InventoryScreen.Awake))]
    public class InventoryScreen_Awake_Patch
    {
        public static void Prefix(InventoryScreen __instance)
        {
            StorageRecycle.CreateComponent<StorageRecycle>(__instance); 
        }
    }
}

using HarmonyLib;
using MGSC;

namespace QM_RecycleHotKey.StorageRecyclePatches
{
    /// <summary>
    /// Attaches a MonoBehaviour to attach the hotkey monitor for the storage recycling functionality.
    /// </summary>
    [HarmonyPatch(typeof(InventoryScreen), nameof(InventoryScreen.Awake))]
    public class InventoryScreen_Awake_Patch
    {
        public static void Prefix(InventoryScreen __instance)
        {
            //Attach an update component since there is no Update on the InventoryScreen itself
            //  Only attaches if it is not already attached
            StorageRecycle storageRecycle = StorageRecycle.CreateComponent<StorageRecycle>(__instance);
        }
    }
}

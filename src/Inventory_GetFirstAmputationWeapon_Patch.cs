using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM_RecycleHotKey
{
    [HarmonyPatch(typeof(Inventory), nameof(Inventory.GetFirstAmputationWeapon))]
    public static class Inventory_GetFirstAmputationWeapon_Patch
    {

        public static bool Prepare()
        {
            //Amputate without weapon wins over the backpack search.
            return !Plugin.Config.AmputateWithoutWeapon && Plugin.Config.AllowAmputationWeaponFromInventory;
        }

        /// <summary>
        /// Finds an amputation weapon in the backpack if one is not found in the quickslots.
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="__result"></param>
        public static void Postfix(Inventory __instance, ref BasePickupItem __result)
        {
            //Check if the default search found a weapon.
            if (__result != null) return;

            //COPY:  This is effectively a modified copy of the original function code.
            ItemStorage storage = __instance.BackpackStore;
            if (storage == null) return;

            //Search the backpack for a compatible weapon.  Sort by price so the cheapest weapon is used first.
            foreach (BasePickupItem item in storage.Items
                .OrderBy(x => x.Record<ItemRecord>()?.Price ?? 0))
            {
                WeaponRecord weaponRecord = item.Record<WeaponRecord>();
                if(weaponRecord != null && weaponRecord.MeleeCanAmputate 
                    && !item.Comp<BreakableItemComponent>().IsBroken)
                {
                    __result = item;
                    return;
                }
            }
        }
    }
}

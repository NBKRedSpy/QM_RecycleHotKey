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
            try
            {
                //Check if the default search found a weapon in hand.
                if (__result != null) return;

                //COPY: Inventory.GetFirstAmputationWeapon - This is effectively a modified copy of the original function code.
                ItemStorage storage = __instance.BackpackStore;
                if (storage == null) return;

                //Search the backpack for a compatible weapon.  
                //Sort:  
                // * bone knife - bone knife costs the same as cheapest knife, but is free.
                // * price
                // * order in backpack.  -- Display order is not the same as inventory position.

                //Sort by price so the cheapest weapon is used first, then by cell position.
                //Then by the display order (X then Y).  Required as the actual item grid uses a cache and is not in display order.
                foreach (BasePickupItem item in storage.Items
                    .OrderByDescending(x => x.Id == "bone_knife")  //Always prefer the bone knife.
                    .ThenBy (x => x.Record<ItemRecord>()?.Price ?? 0)   //Cheapest
                    .ThenBy(x => x.InventoryPos.X)  //Displayed order
                    .ThenBy(x => x.InventoryPos.Y)) 
                {
                    WeaponRecord weaponRecord = item.Record<WeaponRecord>();
                    if (weaponRecord != null && weaponRecord.MeleeCanAmputate
                        && !item.Comp<BreakableItemComponent>().IsBroken)
                    {
                        __result = item;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Logger.LogError(ex);
            }
        }
    }
}

using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM_RecycleHotKey.Patches
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

                //COPY: Inventory.GetFirstAmputationWeapon - This is effectively a modified copy of the original function code.


                //Sort:  
                // * Backpack, then hand.
                // * bone knife - bone knife costs the same as cheapest knife, but is free.
                // * price
                // * order in backpack.  -- Display order is not the same as inventory position.

                List<(bool isHand, BasePickupItem item, int? Durability)> allItems = new List<(bool, BasePickupItem, int?)>();
                allItems.AddRange(__instance.BackpackStore.Items.Select(x => (false, x, GetAmputationDurability(x))));
                allItems.AddRange(__instance.WeaponSlots.SelectMany(x => x.Items).Select(x => (true, x, GetAmputationDurability(x))));


                BasePickupItem amputationWeapon = null;

                //Filter out broken and not amputation capable items.
                allItems = allItems.Where(x => x.Durability.HasValue).ToList();

                //Search for a bone knife with lowest durabillity in backpack then in hand.
                //Try for the lowest durability bone knife.
                amputationWeapon = allItems
                    .Where(x => x.item.Id == "bone_knife")
                    .OrderBy(x => x.isHand)  //Backpack first, then hand.
                    .ThenBy(x => x.Durability) //Prefer the lowest durability.  Primarily because there are mod based sorts.
                    .Select(x => x.item)
                    .FirstOrDefault();      

                if(amputationWeapon != null)
                {
                    __result = amputationWeapon;
                    return;
                }

                //Search for anything otherwise.
                // Preferred order:
                //  * In Hand, then backpack. - Hand overrides anything else.
                //  * Cheapest price
                //  * Display order in backpack.
                amputationWeapon = allItems
                    .OrderByDescending(x => x.isHand)  
                    .ThenBy(x => x.item.Record<ItemRecord>()?.Price ?? 0)   //Cheapest
                    .ThenBy(x => x.item.InventoryPos.Y)
                    .ThenBy(x => x.item.InventoryPos.X)
                    .Select(x=> x.item)
                    .FirstOrDefault();

                if(amputationWeapon != null)
                {
                    __result = amputationWeapon;
                    return;
                }

                //Otherwise, use whatever the original function found.  Will probably be null;
                return;
            }
            catch (Exception ex)
            {
                Plugin.Logger.LogError(ex);
            }
        }

        /// <summary>
        /// Returns the durability of the item.  If the item cannot amputate or is broken, returns null.    
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private static int? GetAmputationDurability(BasePickupItem item)
        {
            WeaponRecord weaponRecord = item.Record<WeaponRecord>();
            if (weaponRecord != null && weaponRecord.MeleeCanAmputate
                && !item.Comp<BreakableItemComponent>().IsBroken)
            {
                return item.Comp<BreakableItemComponent>().Durability;
            }

            return null;
        }

        /// <summary>
        /// Searches for a compatible weapon.  Preferring a bone knife.
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        private static BasePickupItem FindPreferredWeapon(IEnumerable<BasePickupItem> items)
        {
            foreach (BasePickupItem item in items
                .OrderByDescending(x => x.Id == "bone_knife")  //Always prefer the bone knife.
                .ThenBy(x => x.Record<ItemRecord>()?.Price ?? 0)   //Cheapest
                .ThenBy(x => x.InventoryPos.Y)
                .ThenBy(x => x.InventoryPos.X))  //Displayed order
            {
                WeaponRecord weaponRecord = item.Record<WeaponRecord>();
                if (weaponRecord != null && weaponRecord.MeleeCanAmputate
                    && !item.Comp<BreakableItemComponent>().IsBroken)
                {
                    return item;
                }
            }

            return null;
        }
    }
}

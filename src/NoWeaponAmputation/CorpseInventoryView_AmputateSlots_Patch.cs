using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM_RecycleHotKey.NoWeaponAmputation
{
    [HarmonyPatch(typeof(CorpseInventoryView), nameof(CorpseInventoryView.AmputateSlot))]
    internal static class CorpseInventoryView_AmputateSlots_Patch
    {
        public static bool Prepare()
        {
            return Plugin.Config.AmputateWithoutWeapon;
        }

        public static bool Prefix(CorpseInventoryView __instance, ref bool __result , CustomWoundSlot woundSlot)
        {

            //Warning - This is a copy and replace of the base game's code.
            if (!__instance.CanAmputate())
            {
                return false;
            }
            if (!TurnSystem.CanProcessPlayerTurn(__instance._turnController, __instance._turnMetadata, __instance._creatures))
            {
                return false;
            }
            Player player = __instance._creatures.Player;
            BasePickupItem firstAmputationWeapon = player.Inventory.GetFirstAmputationWeapon();

            //code Change
            //string damageType = DamageSystem.GetDamageType(firstAmputationWeapon);
            string damageType = "lacer";  //emulate a heavy axe.
            bool num = AmputationHelper.AmputateCorpse(__instance._mapGrid, __instance._itemsOnFloor, __instance._corpseStorage, woundSlot.WoundSlotType, player.Inventory, player.pos, damageType);

            //----------- Start Code Change
            //if (num)
            //{
            //    firstAmputationWeapon?.Comp<BreakableItemComponent>().Break();
            //    __instance._statistics.IncreaseStatistic(StatisticType.AmputationCorpse);
            //}

            //----------- End Code Change

            __result = num;
            //return num;

            return false;
        }
    }
}

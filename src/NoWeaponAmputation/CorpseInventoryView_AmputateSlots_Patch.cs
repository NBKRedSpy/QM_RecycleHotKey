﻿using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM_RecycleHotKey.NoWeaponAmputation
{
    [HarmonyPatch(typeof(CorpseInspectWindow), nameof(CorpseInspectWindow.AmputateSlot))]
    internal static class CorpseInventoryView_AmputateSlots_Patch
    {
        public static bool Prepare()
        {
            return Plugin.Config.AmputateWithoutWeapon;
        }

        public static bool Prefix(CorpseInspectWindow __instance, ref bool __result , CustomWoundSlot woundSlot)
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
            BasePickupItem firstAmputationWeapon = player.CreatureData.Inventory.GetFirstAmputationWeapon();

            //code Change - emulate a heavy axe and invoke the amputation.

            //original code:  string damageType = DamageSystem.GetDamageType(firstAmputationWeapon);
            string damageType = "lacer";  //emulate a heavy axe.
            bool num = AmputationSystem.AmputateCorpse(__instance._mapGrid, __instance._itemsOnFloor, __instance._corpseStorage, woundSlot.WoundSlotType, player.CreatureData.Inventory, player.CreatureData.Position, damageType);

            //----------- Start Code Change - removes this code.
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

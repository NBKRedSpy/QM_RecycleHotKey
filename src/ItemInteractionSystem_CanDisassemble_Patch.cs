using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGSC;
using HarmonyLib;
using UnityEngine;

namespace QM_RecycleHotKey
{
    /// <summary>
    /// Handles excluding items from being disassembled when recycling a corpse.  Ex: Ammo.
    /// </summary>
    [HarmonyPatch(typeof(ItemInteractionSystem), nameof(ItemInteractionSystem.CanDisassemble))] 
    public static class ItemInteractionSystem_CanDisassemble_Patch
    {
        public static bool Prepare()
        {

            return !Plugin.Config.DoNotRecycleSpecialItems == false;

        }

        public static bool Prefix(BasePickupItem item, ref bool __result)
        {
            PickupItem pickupItem = item as PickupItem;

            if (!CorpseInspectionWindow_DisassemblyAllItems_Patch.DissassembleAllIsRunning
                || pickupItem == null)
            {
                return true;
            }

            bool ignore = false;    


            foreach (ItemRecord record in pickupItem.Records)
            {
                if(Plugin.Config.DoNotRecycleItems.Contains(record.ItemClass))
                {
                    ignore = true;
                    break;
                }
            }

            if (ignore)
            { 
                __result = false; 
                return false;
            }

            //Run the original code.
            return true;
        }
    }
}

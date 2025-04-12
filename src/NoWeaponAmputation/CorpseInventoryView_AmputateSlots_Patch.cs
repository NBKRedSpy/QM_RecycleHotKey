using HarmonyLib;
using MGSC;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static HarmonyLib.Code;

namespace QM_RecycleHotKey.NoWeaponAmputation
{
    [HarmonyPatch(typeof(CorpseInspectWindow), nameof(CorpseInspectWindow.AmputateSlot))]
    internal static class CorpseInventoryView_AmputateSlots_Patch
    {
        public static bool Prepare()
        {
            return Plugin.Config.AmputateWithoutWeapon;
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> originalInstructions = new List<CodeInstruction>();

            //------ IMPORTANT! -------
            //This had to be a transpiler patch because the Filter Items patch had issues with this mod's previous code
            //  which would prevent the original code from executing.


            //Goal:  Override the Damage type with the constant "lacer"
            //  which emulates the user having a cutting weapon that can amputate.
            //  For example, an axe.




            //Code Notes: 
            //  This is a surprisingly easy patch.
            //  * The CanAmputate() check is overridden by a related patch.
            //  * GetFirstAmputationWeapon() just returns null if it can't find a compatible weapon.
            //  * The weapon break check "firstAmputationWeapon?.Comp<BreakableItemComponent>().Break();"
            //      Already has a null conditional check.

            //Search match:
            //// string damageType = DamageSystem.GetDamageType(firstAmputationWeapon);
            //IL_0042: ldloc.1
            //IL_0043: call string MGSC.DamageSystem::GetDamageType(class MGSC.BasePickupItem)
            //IL_0048: stloc.2
            CodeMatcher codeMatcher = new CodeMatcher(instructions)
                .MatchStartForward(
                    CodeMatch.LoadsLocal(),
                    CodeMatch.Calls(() => DamageSystem.GetDamageType(default)),
                    new CodeMatch(OpCodes.Stloc_2)
                )
                .ThrowIfNotMatchForward("Unable to find the 'DamageSystem.GetDamageType' call block")
                .RemoveInstructions(3)

                //Insert set the damage type to "lacer"
                .Insert(
                    new CodeInstruction(OpCodes.Ldstr, "lacer"),
                    CodeInstruction.StoreLocal(2)
                );

            List<CodeInstruction> results = codeMatcher.InstructionEnumeration().ToList();

            return results;
        }
    }
}

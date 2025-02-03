using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MGSC;
using QM_RecycleHotKey;
using UnityEngine;

namespace QM_RecycleHotKey
{
    [HarmonyPatch(typeof(CorpseInspectWindow), nameof(CorpseInspectWindow.Update))]
    internal static class CorpseInventoryView_Update_Patch
    {
        public static bool MoveToBackpack { get; set; } = false;

        public static void Postfix(CorpseInspectWindow __instance)
        {
            bool isRecycleCurrent = false;

            if (Input.GetKey(Plugin.Config.RecycleCurrentPageKey))
            {
                isRecycleCurrent = true;
            }
            else if (Input.GetKey(Plugin.Config.AmputateKey))
            {
                isRecycleCurrent = false;
            }
            else
            {
                return;
            }

            if (isRecycleCurrent)
            {
                if (__instance._disassemblyCorpseButton.isActiveAndEnabled)
                {
                    __instance.DisassemblyButtonClick();
                }
            }

            if(!isRecycleCurrent || Plugin.Config.RecycleAlsoAmputates)
            {
                //Amputate
                if (__instance._bodyPartsButton.isActiveAndEnabled)
                {
                    var startPage = __instance._activeCorpseScreenPageType;

                    __instance._bodyPartsButton.OnPointerClick(null);

                    if (__instance._disassemblyCorpseButton.isActiveAndEnabled)
                    {
                        __instance._disassemblyCorpseButton.OnPointerClick(null);
                    }

                    if (startPage != CorpseInspectWindow.ActiveCorpseScreenPage.BodyParts)
                    {
                        //move back to backpack page.
                        if (__instance._itemsButton.isActiveAndEnabled)
                        {
                            __instance._itemsButton.OnPointerClick(null);
                        }
                    }
                }
            }
        }
    }
}

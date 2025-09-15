using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static MGSC.CorpseInspectWindow;

namespace QM_RecycleHotKey
{
    /// <summary>
    /// Update() hook for hotkeys, and processes the amputate and recycle hotkeys.
    /// </summary>
    public class CorpseInspectWindow_Hook : MonoBehaviour
    {
        public CorpseInspectWindow CorpseInspectWindow { get; set; }


        public void Update()
        {
            CorpseUpdate(CorpseInspectWindow);
        }

        public static void CorpseUpdate(CorpseInspectWindow __instance)
        {
            try
            {

                bool isRecycleCurrent = false;

                if (Input.GetKeyDown(Plugin.Config.RecycleCurrentPageKey))
                {
                    isRecycleCurrent = true;
                }
                else if (Input.GetKeyDown(Plugin.Config.AmputateKey))
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

                if (!isRecycleCurrent || Plugin.Config.RecycleAlsoAmputates)
                {
                    //Amputate
                    if (__instance._bodyPartsButton.isActiveAndEnabled)
                    {
                        ActiveCorpseScreenPage startPage = __instance._currentPage;

                        __instance._bodyPartsButton.OnPointerClick(null);

                        if (__instance._disassemblyCorpseButton.isActiveAndEnabled)
                        {
                            __instance._disassemblyCorpseButton.OnPointerClick(null);
                        }

                        if (startPage != ActiveCorpseScreenPage.BodyParts)
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
            catch (Exception ex)
            {
                Plugin.Logger.LogError(ex);
            }
        }

    }
}

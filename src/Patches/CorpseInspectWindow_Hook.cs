using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using static MGSC.CorpseInspectWindow;

namespace QM_RecycleHotKey.Patches
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

                if(!Input.anyKeyDown) return;

                bool runRecycle = false;   //Will invoke the recycle functionality

                if (InputHelper.GetKeyDown(Plugin.Config.TakeAndRecyclePage))
                {
                    runRecycle = true;

                    if (__instance._takeAllButton.isActiveAndEnabled)
                    {

                        //The take all button checks the left button click, unlike the other click methods.
                        __instance._takeAllButton.OnPointerClick(
                            new PointerEventData(EventSystem.current)
                            {
                                button = PointerEventData.InputButton.Left
                            });
                    }
                }
                else if (InputHelper.GetKeyDown(Plugin.Config.RecycleCurrentPageKey))
                {
                    runRecycle = true;
                }
                else if (InputHelper.GetKeyDown(Plugin.Config.AmputateKey))
                {
                    runRecycle = false;
                }
                else
                {
                    return;
                }

                if (runRecycle)
                {
                    if (__instance._disassemblyCorpseButton.isActiveAndEnabled)
                    {
                        __instance.DisassemblyButtonClick();
                    }
                }

                if (!runRecycle || Plugin.Config.RecycleAlsoAmputates)
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

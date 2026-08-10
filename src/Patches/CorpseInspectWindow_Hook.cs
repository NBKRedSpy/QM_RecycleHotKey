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

                bool recycle = false;   //Invokes the recycle functionality
                bool takeAll = false;      //Invokes the take all functionality



                if (InputHelper.GetKeyDown(Plugin.Config.RecycleAndTakeCurrentPageKey))
                {
                    recycle = true;
                    takeAll = true;
                }
                else if (InputHelper.GetKeyDown(Plugin.Config.RecycleCurrentPageKey))
                {
                    recycle = true;
                }
                else if (InputHelper.GetKeyDown(Plugin.Config.AmputateKey))
                {
                    recycle = false;
                }
                else
                {
                    return;
                }

                if (recycle)
                {
                    if (__instance._disassemblyCorpseButton.isActiveAndEnabled)
                    {
                        __instance.DisassemblyButtonClick();
                    }
                }

                if (!recycle || Plugin.Config.RecycleAlsoAmputates)
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

                //The take all button checks the left button click, unlike the other click methods.
                if (takeAll)
                {
                    __instance._takeAllButton.OnPointerClick(
                    new PointerEventData(EventSystem.current)
                    {
                        button = PointerEventData.InputButton.Left
                    });
                }

            }
            catch (Exception ex)
            {
                Plugin.Logger.LogError(ex);
            }
        }

    }
}

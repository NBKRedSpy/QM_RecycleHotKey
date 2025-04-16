using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MGSC;
using UnityEngine;

namespace QM_RecycleHotKey
{
    /// <summary>
    /// Attaches hotkey detection for the Sort and Amputate functionality.
    /// </summary>
    [HarmonyPatch(typeof(CorpseInspectWindow), nameof(CorpseInspectWindow.Configure))]
    public static class CorpseInspectWindow_Configure_Patch
    {

        public static void Prefix(CorpseInspectWindow __instance)
        {
            const string GameObjectName = "SortToTabsUpdateObject";

            if (__instance.GetComponent<CorpseInspectWindow_Hook>() != null) return;

            CorpseInspectWindow_Hook update = __instance.gameObject.AddComponent<CorpseInspectWindow_Hook>();
            update.name = GameObjectName;
            update.CorpseInspectWindow = __instance;
        }
    }
}

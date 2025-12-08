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
    [HarmonyPatch(typeof(CorpseInspectWindow), nameof(CorpseInspectWindow.DisassemblyAllItems))]
    public static class CorpseInspectionWindow_DisassemblyAllItems_Patch
    {

        /// <summary>
        /// Indicates the disassemble all is running by setting the DissassembleAllIsRunning state flag.  Used to prevent ammo from being disassembled
        /// in the process.
        /// </summary>
        public static bool DissassembleAllIsRunning { get; set; }

        public static bool Prepare()
        {
            return Plugin.Config.DoNotRecycleSpecialItems;
        }

        public static void Prefix()
        {
            DissassembleAllIsRunning = true;
        }

        public static void Postfix()
        {
            DissassembleAllIsRunning = false;
        }
    }
}

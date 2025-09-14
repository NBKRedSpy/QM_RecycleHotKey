using HarmonyLib;
using ModConfigMenu;
using ModConfigMenu.Objects;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;

namespace QM_RecycleHotKey
{
    internal class McmConfiguration : McmConfigurationBase
    {

        public McmConfiguration(ModConfig config) : base (config) { }

        public override void Configure()
        {
            ModConfig defaults = new ModConfig();

            ModConfigMenuAPI.RegisterModConfig("Recycle Hotkey", new List<ConfigValue>()
            {
                new ConfigValue("__RestartNote", @"<color=#FBE343>The game must be restarted if any changes are made.</color>" , "Restart"),

                CreateConfigProperty(nameof(ModConfig.RecycleAlsoAmputates),
                    "When recycling items, the amputation will also occur.",
                    "Recycle Also Amputates"),
                CreateConfigProperty(nameof(ModConfig.AmputateWithoutWeapon),
                    "Will execute the amputation action without requiring or using a weapon that can amputate.",
                    "Amputate Without Weapon"),
                CreateConfigProperty(nameof(ModConfig.AllowAmputationWeaponFromInventory),
                    """
                    If enabled, allows amputation weapons to be used directly from the inventory instead of requiring them to be equipped. 
                    If 'Amputate Without Weapon' is enabled, this option is ignored.

                    Weapon selection priority: Quick slots first, then items in the backpack, sorted by price (cheapest first).
                    If multiple items have the same price, the weapon closest to the upper left of the storage will be used.
                    """,
                    "Allow Amputation Weapon From Inventory"),
                
                CreateConfigProperty(nameof(ModConfig.DoNoCloseWindowOnEmpty),
                    "Will not automatically close the corpse window if there is more than one tab. Including a new tab created by dropping items.",
                    "Do Not Close Window On Empty"),
                CreateConfigProperty(nameof(ModConfig.DoNotRecycleSpecialItems),
                    "Recycle will not recycle items in the DoNotRecycleItems list.",
                    "Do Not Recycle Special Items"),

                CreateReadOnly(nameof(ModConfig.AmputateKey)),
                CreateReadOnly(nameof(ModConfig.RecycleCurrentPageKey)),
                CreateReadOnly(nameof(ModConfig.DoNotRecycleItems)),

            }, OnSave);
        }
         
    }
}

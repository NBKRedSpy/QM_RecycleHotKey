using HarmonyLib;
using ModConfigMenu;
using ModConfigMenu.Contracts;
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

namespace QM_RecycleHotKey.Mcm
{
    internal class McmConfiguration : McmConfigurationBase
    {

        public McmConfiguration(ModConfig config) : base (config) { }

        public override void Configure()
        {
            ModConfig defaults = new ModConfig();

            ModConfigMenuAPI.RegisterModConfig("Recycle Hotkey", new List<IConfigValue>()
            {
                new ConfigValue("__RestartNote", @"<color=#FF0000>The game must be restarted if any changes are made.</color>" , "Restart"),

                CreateConfigProperty(nameof(ModConfig.RecycleAlsoAmputates),
                    "When recycling items, the amputation will also occur.",
                    "Recycle Also Amputates"),
                CreateConfigProperty(nameof(ModConfig.AmputateWithoutWeapon),
                    "Will execute the amputation action without requiring or using a weapon that can amputate.",
                    "Amputate Without Weapon"),
                CreateConfigProperty(nameof(ModConfig.AllowAmputationWeaponFromInventory),
                    """
                    If enabled, will allow amputation weapons to be used from the inventory instead of requiring them to be equipped. 
                    If 'Amputate Without Weapon' is enabled, this option is ignored. The weapon priority is: Quick slots first, then items in backpack, sorted by price. Bones treated as if they have zero price. If there are multiple items with the same price, the weapon that is closest to the upper left of the storage will be used.
                    """,
                    "Allow Amputation Weapon From Inventory"),
                
                CreateConfigProperty(nameof(ModConfig.DoNoCloseWindowOnEmpty),
                    """
                    When using Take All, normally the game will close the window if the *current* tab is empty.  
                    This option keeps the window open if there is more than one tab. Important for a recycling a corpse's body
                    where recycling creates a new tabs because not all the items could fit in the user's inventory at the time.
                    """,
                    "Do Not Close Window On Empty"),
                CreateConfigProperty(nameof(ModConfig.DoNotRecycleSpecialItems),
                    "Recycle will not recycle items in the DoNotRecycleItems list.",
                    "Do Not Recycle Special Items"),

                CreateEnumDropdown<KeyCode>(nameof(ModConfig.AmputateKey),
                    "The key to press to amputate the selected body part.",
                    "Amputate Key", sort: true),
                CreateEnumDropdown<KeyCode>(nameof(ModConfig.RecycleCurrentPageKey), 
                    "The key to press to recycle all items on the current page.",
                    "Recycle Current Page Key", sort: true),

                CreateEnumDropdown<KeyCode>(nameof(ModConfig.TakeAndRecyclePage),
                    "The key to press to take all items and then recycle the remaining items on the current page.",
                    "Recycle And Take Current Page Key", sort: true),


                CreateReadOnly(nameof(ModConfig.DoNotRecycleItems))

            }, OnSave);
        }
         
    }
}

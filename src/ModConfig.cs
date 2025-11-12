using MGSC;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using QM_RecycleHotKey.Mcm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_RecycleHotKey
{
    public class ModConfig : PersistentConfig<ModConfig>
    {
        public ModConfig()
        {
        }

        public ModConfig(string configPath) : base(configPath)
        {

        }

        /// <summary>
        /// The list of items that will not be recycled.
        /// </summary>
        [JsonProperty(ItemConverterType =typeof(StringEnumConverter))]
        public HashSet<ItemClass> DoNotRecycleItems { get; set; } = new HashSet<ItemClass>()
        {
            ItemClass.RepairKit,
            ItemClass.Ammo,
            ItemClass.Mine,
            ItemClass.Grenade,
            //Most of the repair kits.  Such as armor or firearm repair kits.
            ItemClass.RepairKit,
            //Contains some repair kits that are not tagged as RepairKit.  For example, hi-tech repair kit or quasimorph repair kit.
            //  Shouldn't affect anything as regular parts should not break down.
            ItemClass.Parts,        

        };

        /// <summary>
        /// If true, will not automatically close the corpse window if there is more than one tab.  Including a new tab 
        /// created by dropping items.
        /// </summary>
        public bool DoNoCloseWindowOnEmpty { get; set; } = false;

        /// <summary>
        /// If true, will allow amputation weapons to be used from the inventory
        /// instead of requiring them to be equipped.
        /// </summary>
        public bool AllowAmputationWeaponFromInventory { get; set; }

        /// <summary>
        /// Recycle will not recycle items in the DoNotRecycleItems list.
        /// </summary>
        public bool DoNotRecycleSpecialItems { get; set; } = true;

        [JsonConverter(typeof(StringEnumConverter))]
        public KeyCode RecycleCurrentPageKey { get; set; } = KeyCode.Z;

        [JsonConverter(typeof(StringEnumConverter))]
        public KeyCode AmputateKey { get; set; } = KeyCode.A;

        /// <summary>
        /// If true, will execute the amputation action without requiring or using a 
        /// weapon that can amputate.
        /// </summary>
        public bool AmputateWithoutWeapon { get; set; } = false;

        /// <summary>
        /// When recycling items, the amputation will also occur.
        /// </summary>
        public bool RecycleAlsoAmputates { get; set; } = false;


        public void Save(string configPath)
        {
            string json = JsonConvert.SerializeObject(this, SerializerSettings);
            File.WriteAllText(Plugin.ConfigDirectories.ConfigPath, json);
        }
    }
}

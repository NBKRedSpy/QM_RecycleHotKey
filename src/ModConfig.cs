using MGSC;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_RecycleHotKey
{
    public class ModConfig
    {

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

        public static ModConfig LoadConfig(string configPath)
        {
            ModConfig config;

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
            };

            if (File.Exists(configPath))
            {
                try
                {
                    string sourceJson = File.ReadAllText(configPath);

                    config = JsonConvert.DeserializeObject<ModConfig>(sourceJson, serializerSettings);

                    //Add any new elements that have been added since the last mod version the user had.

                    string upgradeConfig = JsonConvert.SerializeObject(config, serializerSettings);

                    if(upgradeConfig != sourceJson)
                    {
                        Debug.Log("Updating config with missing elements");
                        //re-write
                        File.WriteAllText(configPath, upgradeConfig);
                    }

                    return config;
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error parsing configuration.  Ignoring config file and using defaults");
                    Debug.LogException(ex);

                    //Not overwriting in case the user just made a typo.
                    config = new ModConfig();
                    return config;
                }
            }
            else
            {
                config = new ModConfig();

                string json = JsonConvert.SerializeObject(config, serializerSettings);
                File.WriteAllText(configPath, json);

                return config;
            }
        }

    }
}

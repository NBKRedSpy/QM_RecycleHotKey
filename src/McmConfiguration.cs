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
    internal class McmConfiguration
    {

        public ModConfig Config { get; set; }

        /// <summary>
        /// Used to set the defaults established by the ModConfig class.
        /// </summary>
        private ModConfig Defaults { get; set; }

        /// <summary>
        /// Used to make the keys for read only entries unique.
        /// </summary>
        private static int UniqueId = 0;    

        public McmConfiguration(ModConfig config)
        {
            Config = config;
            Defaults = new ModConfig();
        }

        /// <summary>
        /// Attempts to configure the MCM, but logs an error and continues if it fails.
        /// </summary>
        public bool TryConfigure()
        {
            try
            {
                Configure();
                return true;
            }
            catch (FileNotFoundException)
            {
                Plugin.Logger.Log("Bypassing MCM. The 'Mod Configuration Menu' mod is not loaded. ");
            }
            catch (Exception ex)
            {
                Plugin.Logger.LogError(ex, "An error occurred when configuring MCM");
            }

            return false;


        }
        public void Configure()
        {
            ModConfig defaults = new ModConfig();

            ModConfigMenuAPI.RegisterModConfig("Recycle Hotkey", new List<ConfigValue>()
            {
                CreateConfigProperty(nameof(ModConfig.RecycleAlsoAmputates),
                    "When recycling items, the amputation will also occur.",
                    "Recycle Also Amputates"),
                CreateConfigProperty(nameof(ModConfig.AmputateWithoutWeapon),
                    "Will execute the amputation action without requiring or using a weapon that can amputate.",
                    "Amputate Without Weapon"),
                CreateConfigProperty(nameof(ModConfig.DoNoCloseWindowOnEmpty),
                    "Will not automatically close the corpse window if there is more than one tab. Including a new tab created by dropping items.",
                    "Do No Close Window On Empty"),
                CreateConfigProperty(nameof(ModConfig.DoNotRecycleSpecialItems),
                    "Recycle will not recycle items in the DoNotRecycleItems list.",
                    "Do Not Recycle Special Items"),
                new ConfigValue("__RestartNote", "Changes will require a restart to take effect.", "Restart"),

                CreateReadOnly(nameof(ModConfig.AmputateKey)),
                CreateReadOnly(nameof(ModConfig.RecycleCurrentPageKey)),
                CreateReadOnly(nameof(ModConfig.DoNotRecycleItems)),

            }, OnSave);
        }


        private ConfigValue CreateReadOnly(string propertyName, string header = "Only available in config file")
        {
            int key = UniqueId++;

            //Since the UI uppercases the text, add spaces to make it easier to read.
            Regex regex = new Regex(@"([A-Z0-9])");

            object value = AccessTools.Property(typeof(ModConfig), propertyName).GetValue(this.Config);

            string formattedValue;

            if (value == null)
            {
                value = "Null";
            }
            if(value is System.Collections.IEnumerable enumList)
            {
                List<string> list = new();

                foreach (var item in enumList)
                {
                    list.Add(item.ToString());
                }

                formattedValue = string.Join(",", list);
            }
            else
            {
                formattedValue = value.ToString();
            }

            string formattedPropertyName = regex.Replace(propertyName.ToString(), " $1").TrimStart();

            return new ConfigValue(key.ToString(), $@"{formattedPropertyName}: {formattedValue}", header);

        }



        private ConfigValue CreateConfigProperty<T>(string propertyName,
            string tooltip, string label, T min, T max, string header = "General") where T: struct
        {
            T defaultValue = (T)AccessTools.Property(typeof(ModConfig), propertyName).GetValue(Defaults);
            T propertyValue = (T)AccessTools.Property(typeof(ModConfig), propertyName).GetValue(this.Config);

            switch (typeof(T))
            {
                case Type floatType when floatType == typeof(float):

                    return new ConfigValue(propertyName, propertyValue, header, defaultValue, 
                        tooltip, label, Convert.ToSingle(min), Convert.ToSingle(max));
                case Type intType when intType == typeof(int):
                    return new ConfigValue(propertyName, propertyValue, header, defaultValue,
                        tooltip, label, Convert.ToInt32(min), Convert.ToInt32(max));
                default:
                    throw new ApplicationException($"Unexpected numeric type '{typeof(T).Name}'");
            }
        }

        private ConfigValue CreateConfigProperty(string propertyName,
            string tooltip, string label, string header = "General")
        {
            object defaultValue = AccessTools.Property(typeof(ModConfig), propertyName).GetValue(Defaults);
            object propertyValue = AccessTools.Property(typeof(ModConfig), propertyName).GetValue(this.Config);

            return new ConfigValue(propertyName, propertyValue, header, defaultValue, tooltip, label);
        }


        /// <summary>
        /// Sets the ModConfig's property that matches the ConfigValue key.
        /// Returns false if the property could not be found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        private bool SetModConfigValue(string key, object value)
        {
            //TODO: Refactor this to a class.
            //TODO: continuing if there is no match since items like the "Must restart" note is not a property.
            MethodInfo setter = AccessTools.PropertySetter(typeof(ModConfig), key);
            if (setter == null) return false;

            setter.Invoke(Config, new object[] { value});
            return true;
        }

        private bool OnSave(Dictionary<string, object> currentConfig, out string feedbackMessage)
        {
            feedbackMessage = "";

            foreach (var entry in currentConfig)
            {
                SetModConfigValue(entry.Key, entry.Value);
            }

            Config.Save(Plugin.ConfigDirectories.ConfigPath);

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UI.Utilities
{
    /// <summary>
    /// Reads configuration data from a JSON file.
    /// </summary>
    public static class ConfigReader
    {
        private static readonly string configPath;
        private static dynamic configData;

        static ConfigReader()
        {
            try
            {
                string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
                configPath = Path.Combine(projectRoot, "test-config.json");

                if (!File.Exists(configPath))
                {
                    throw new FileNotFoundException($"JSON file not found: {configPath}");
                }

                string jsonContent = File.ReadAllText(configPath);
                configData = JsonConvert.DeserializeObject<dynamic>(jsonContent);

                if (configData == null)
                {
                    throw new Exception("JSON file is corrupted or invalid.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading JSON configuration file: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves Drag & Drop test items from the configuration file.
        /// </summary>
        /// <returns>Dictionary of menu item IDs and their corresponding names.</returns>
        public static Dictionary<string, string> GetDragMenuItems()
        {
            Dictionary<string, string> menuItems = new Dictionary<string, string>();
            try
            {
                JArray itemsArray = (JArray)configData["TestCaseParameters"]["DragAndDropFunctionalityTests"]["DragMenuItems"];

                foreach (JObject itemObj in itemsArray)
                {
                    foreach (var item in itemObj)
                    {
                        menuItems.Add(item.Key, item.Value.ToString());
                    }
                }

                LoggerHelper.LogInfo($"Loaded {menuItems.Count} menu items from configuration.");
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError($"Error reading menu items from JSON: {ex.Message}");
            }

            return menuItems;
        }
    }
}

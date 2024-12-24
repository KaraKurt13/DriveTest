using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;

namespace Assets.Scripts.SaveData
{
    public static class SaveSystem
    {
        public static PlayerSaveData SaveData;

        private static string _savePath = Path.Combine(Application.persistentDataPath, "save.json");

        public static void SavePlayerData()
        {
            string json = JsonConvert.SerializeObject(SaveData);
            File.WriteAllText(_savePath, json);
            Debug.Log("Data saved");
        }

        public static void LoadPlayerData()
        {
            if (File.Exists(_savePath))
            {
                string json = File.ReadAllText(_savePath);
                SaveData = JsonConvert.DeserializeObject<PlayerSaveData>(json);
            }
            else
            {
                SaveData = new PlayerSaveData();
                SavePlayerData();
            }
            Debug.Log($"Data loaded = {SaveData} ");
        }
    }
}
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
        public static PlayerSaveData PlayerData;

        private static string _savePath = Path.Combine(Application.persistentDataPath, "save.json");

        public static void SavePlayerData()
        {
            string json = JsonConvert.SerializeObject(PlayerData);
            File.WriteAllText(_savePath, json);
            Debug.Log("Data saved");
        }

        public static void LoadPlayerData()
        {
            if (File.Exists(_savePath))
            {
                string json = File.ReadAllText(_savePath);
                PlayerData = JsonConvert.DeserializeObject<PlayerSaveData>(json);
            }
            else
            {
                PlayerData = new PlayerSaveData();
                SavePlayerData();
            }
            Debug.Log($"Data loaded = {PlayerData} ");
        }
    }
}
using _Project.Scripts.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        public PersistentData Load()
        {
            string json = PlayerPrefs.GetString(ProgressKey);
            Debug.Log($"Loaded {json}");
            return JsonConvert.DeserializeObject<PersistentData>(json);
        }

        public void Save(PersistentData data)
        {
            string json = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(ProgressKey, json);
            Debug.Log($"Saved {json}");
        }
    }
}
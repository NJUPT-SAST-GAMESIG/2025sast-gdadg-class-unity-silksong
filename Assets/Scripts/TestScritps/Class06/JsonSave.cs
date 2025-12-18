using UnityEngine;
using System.IO;

public class JsonSave : MonoBehaviour
{
    [SerializeField] Transform player;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    void Save()
    {
        SaveData saveData= new SaveData();
        saveData.PlayerPosition = player.position;
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("Game Saved");
    }
    
    void Load()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/savefile.json");
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);
        player.position = saveData.PlayerPosition;
        Debug.Log("Game Loaded");
    }
}

public class SaveData
{
    public Vector3 PlayerPosition;
}
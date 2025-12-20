using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save1 : MonoBehaviour
{
    [SerializeField] private Transform player;
    
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
        UnityEngine.PlayerPrefs.SetFloat("PlayerX", player.position.x);
        UnityEngine.PlayerPrefs.SetFloat("PlayerY", player.position.y);
        UnityEngine.PlayerPrefs.SetFloat("PlayerZ", player.position.z);
    }

    void Load()
    {
        float x = UnityEngine.PlayerPrefs.GetFloat("PlayerX");
        float y = UnityEngine.PlayerPrefs.GetFloat("PlayerY");
        float z = UnityEngine.PlayerPrefs.GetFloat("PlayerZ");
        player.position = new Vector3(x, y, z);
    }
}

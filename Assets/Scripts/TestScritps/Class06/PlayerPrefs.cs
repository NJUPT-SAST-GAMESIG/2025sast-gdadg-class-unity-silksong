using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPrefs : MonoBehaviour
{

    [SerializeField] private Transform Player;
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

    public void Save()
    {
        UnityEngine.PlayerPrefs.SetFloat("PlayerX", Player.position.x);
        UnityEngine.PlayerPrefs.SetFloat("PlayerY", Player.position.y);
        UnityEngine.PlayerPrefs.SetFloat("PlayerZ", Player.position.z);
        Debug.Log("Game Saved"+ UnityEngine.PlayerPrefs.GetFloat("PlayerX"));
    }
    
    public void Load()
    {
        Debug.Log("Loaded Ready"+ UnityEngine.PlayerPrefs.GetFloat("PlayerX"));
        float x = UnityEngine.PlayerPrefs.GetFloat("PlayerX");
        float y = UnityEngine.PlayerPrefs.GetFloat("PlayerY");
        float z = UnityEngine.PlayerPrefs.GetFloat("PlayerZ");
        Player.position = new Vector3(x, y, z);
        Debug.Log("Game Loaded"+ Player.transform.position.x);
    }
}

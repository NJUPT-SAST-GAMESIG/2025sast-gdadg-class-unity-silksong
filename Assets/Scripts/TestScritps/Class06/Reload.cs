using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    void Update()
    {
        if  (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            string currentScene = SceneManager.GetActiveScene().path;
            Debug.Log(currentScene);
        }
    }
    
    
}

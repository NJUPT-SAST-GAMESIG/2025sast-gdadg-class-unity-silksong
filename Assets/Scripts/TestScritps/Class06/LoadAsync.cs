using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadAsync : MonoBehaviour
{

    [SerializeField] private GameObject loading;
    [SerializeField] Slider progressBar;
    [SerializeField] TMP_Text progressText;
    [SerializeField] private GameObject mainmenu;
    
    public void LoadSceneAsync()
    {
        StartCoroutine(LoadAsyncScene("class_06"));
    }

    IEnumerator LoadAsyncScene(string scenename)
    {
        mainmenu.SetActive(false);
        loading.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenename);
        
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            progressText.text = progress * 100 + "%";
            progressBar.value = progress;
            if (asyncLoad.progress >= 0.9f)
            {
                progressText.text = "Press any key to continue";
                if (Input.anyKeyDown)
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }
            
            yield return null;
        }
    }
}

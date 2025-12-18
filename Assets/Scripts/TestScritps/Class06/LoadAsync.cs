using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LoadAsync : MonoBehaviour
{

    [SerializeField] private GameObject loading;
    [SerializeField] Slider progressBar;
    [SerializeField] TMP_Text progressText;
    [SerializeField] private GameObject mainmenu;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LoadSceneAsync()
    {
        StartCoroutine(LoadAsyncScene("class_06"));
        

    }

    IEnumerator LoadAsyncScene(string scenename)
    {
        mainmenu.SetActive(false);
        loading.SetActive(true);
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scenename);
        
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

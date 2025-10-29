using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン変更用ボタン
/// </summary>
public class SceneChanger : MonoBehaviour
{

    [SerializeField]
    private LoadSceneName loadSceneName;

  
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void TitleBack()
    {
       
        loadSceneName.SceneName = "TitleScene";
        StartCoroutine(LoadSceneAsync("LoadScene"));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

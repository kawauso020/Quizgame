using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    [SerializeField]
    private LoadSceneName loadSceneName;

    [SerializeField] 
    private ClearTimeSO clearTimeSO;

    [SerializeField]
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(clearTimeSO.FirstClearTime);
        StartCoroutine(LoadNextSceneAsync());
    }

    // Update is called once per frame
    IEnumerator LoadNextSceneAsync()
    {
        // 前のロードシーンをアンロード
        //SceneManager.UnloadSceneAsync("LoadScene");

        // 次のシーンを非同期で読み込み
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadSceneName.SceneName);

        while (!asyncLoad.isDone)
        {
            var progressVal = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            slider.value = progressVal;
            yield return null;
        }
    }
}

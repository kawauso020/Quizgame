using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// このクラスの説明を書いて！！！
/// </summary>
public class SceneLoad : MonoBehaviour
{
    [SerializeField]
    private LoadSceneName loadSceneName;

    [SerializeField] 
    private ClearTimeSO clearTimeSO;

    [SerializeField]
    private Slider slider;

    void Start()
    {
        Debug.Log(clearTimeSO.FirstClearTime);
        StartCoroutine(LoadNextSceneAsync());
    }
    IEnumerator LoadNextSceneAsync()
    {
        //コメントで残す必要は？
        // 前のロードシーンをアンロード
        //SceneManager.UnloadSceneAsync("LoadScene");

        // 次のシーンを非同期で読み込み
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadSceneName.SceneName);

        while (!asyncLoad.isDone)
        {
            var progressVal = Mathf.Clamp01(asyncLoad.progress / 0.9f); //マジックナンバーです。変数化してください。
            slider.value = progressVal;
            yield return null;
        }
    }
}

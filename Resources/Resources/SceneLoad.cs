using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ���̃N���X�̐����������āI�I�I
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
        //�R�����g�Ŏc���K�v�́H
        // �O�̃��[�h�V�[�����A�����[�h
        //SceneManager.UnloadSceneAsync("LoadScene");

        // ���̃V�[����񓯊��œǂݍ���
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadSceneName.SceneName);

        while (!asyncLoad.isDone)
        {
            var progressVal = Mathf.Clamp01(asyncLoad.progress / 0.9f); //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
            slider.value = progressVal;
            yield return null;
        }
    }
}

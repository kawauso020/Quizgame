using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// このクラスの説明を書いて！！！
/// </summary>
public class CountDown : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI limitTimeText;

    [SerializeField]
    private int changeFrame = 3600; // 例: 60FPS × 60秒

    [SerializeField]
    private GameObject timeUpImage;

    [SerializeField]
    private AudioSource currentBGM;      // 今までのBGM
    [SerializeField]
    private AudioSource newBGM;          // 新しく流すBGM

    private float timeValue = 0f;
    private bool changingOrder = false;

    void Start()
    {
        if (timeUpImage != null)
            timeUpImage.SetActive(false);

        if (newBGM != null)
            newBGM.Stop(); // 念のため
    }

    void FixedUpdate()
    {
        timeValue += 0.017f; //マジックナンバーです。変数化してください。

        if (!changingOrder)
        {
            int remainingSeconds = Mathf.Max(0, (int)(changeFrame / 60 - timeValue)); // マジックナンバーです。変数化してください。
            limitTimeText.text = $"のこり<size=0.6><color=yellow>{remainingSeconds}</color></size>びょう";

            if (remainingSeconds == 0)
            {
                changingOrder = true;

                if (timeUpImage != null)
                    timeUpImage.SetActive(true);

                // BGM 切り替え
                if (currentBGM != null && currentBGM.isPlaying)
                    currentBGM.Stop();

                if (newBGM != null)
                    newBGM.Play();

                timeValue = -1f;
            }
        }
    }
}
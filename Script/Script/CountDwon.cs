using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDwon : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI limitTimeText;

    [SerializeField]
    private int changeFrame = 3600; // ó·: 60FPS Å~ 60ïb

    [SerializeField]
    private GameObject timeUpImage;

    [SerializeField]
    private AudioSource currentBGM;      // ç°Ç‹Ç≈ÇÃBGM
    [SerializeField]
    private AudioSource newBGM;          // êVÇµÇ≠ó¨Ç∑BGM

    private float timeValue = 0f;
    private bool changingOrder = false;

    void Start()
    {
        if (timeUpImage != null)
            timeUpImage.SetActive(false);

        if (newBGM != null)
            newBGM.Stop(); // îOÇÃÇΩÇﬂ
    }

    void FixedUpdate()
    {
        timeValue += 0.017f;

        if (!changingOrder)
        {
            int remainingSeconds = Mathf.Max(0, (int)(changeFrame / 60 - timeValue));
            limitTimeText.text = $"ÇÃÇ±ÇË<size=0.6><color=yellow>{remainingSeconds}</color></size>Ç—ÇÂÇ§";

            if (remainingSeconds == 0)
            {
                changingOrder = true;

                if (timeUpImage != null)
                    timeUpImage.SetActive(true);

                // BGM êÿÇËë÷Ç¶
                if (currentBGM != null && currentBGM.isPlaying)
                    currentBGM.Stop();

                if (newBGM != null)
                    newBGM.Play();

                timeValue = -1f;
            }
        }
    }
}
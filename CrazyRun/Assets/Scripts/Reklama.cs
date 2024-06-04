using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class Reklama : MonoBehaviour
{
    public GameObject reklamaPanel;
    public TMP_Text timerText;
    public float timer;
    public float StartTime;

    public GameObject[] buttons;


    void Update()
    {
        timer -= Time.unscaledDeltaTime;
        if (timer < 4)
        {
            Time.timeScale = 0;
            reklamaPanel.SetActive(true);
            if (YandexGame.lang == "ru")
                timerText.text = Mathf.Round(timer).ToString() + " с.";
            if (YandexGame.lang == "en")
                timerText.text = Mathf.Round(timer).ToString() + " s.";
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].activeInHierarchy)
                {
                    buttons[i].SetActive(false);
                }
            }
        }
        if (timer < 0)
        {
            GameObject.Find("Player").GetComponent<PlayerMove>().canMove = true;
            reklamaPanel.SetActive(false);
            YandexGame.FullscreenShow();
            timer = StartTime;
            Time.timeScale = 1;
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Stars : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] TMP_Text killsText;
    [SerializeField] Image stars;
    int index;
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        stars.sprite = sprites[index];
        killsText.text = YandexGame.savesData.maxCount.ToString();

        if (YandexGame.savesData.maxCount > 50)
        {
            index = 1;
        }
        else if (YandexGame.savesData.maxCount > 200)
        { index = 2; }
        else if (YandexGame.savesData.maxCount > 300)
        { index = 3; }
    }
}

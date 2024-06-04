using UnityEngine;
using YG;

public class RewardShow : MonoBehaviour
{
    public GameObject farms;

    // Подписываемся на событие открытия рекламы в OnEnable
    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;

    // Отписываемся от события открытия рекламы в OnDisable
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    // Подписанный метод получения награды
    void Rewarded(int id)
    {

        if (id == 1)
        {
            GameObject.Find("GuiGame").GetComponent<GuiGame>().Reward();
        }
        else if (id == 2)
        {
            WateringAll();
        }
        else if (id == 3)
        {
            HarvestingAll();
        }

    }

    // Метод для вызова видео рекламы
    public void OpenReward(int id)
    {
        YandexGame.RewVideoShow(id);
    }

    public void WateringAll()
    {
        foreach (Transform item in farms.transform)
        {
            if (item.gameObject.activeInHierarchy)
            {
                var crop = item.transform.GetChild(0).GetComponent<Crops>();
                crop.grow = 0;
                crop.startGrow = true;
            }

        }
    }

    public void HarvestingAll()
    {
        foreach (Transform item in farms.transform)
        {
            if (item.gameObject.activeInHierarchy)
            {
                var crop = item.transform.GetChild(0).GetComponent<Crops>();
                crop.grow = 0;
                crop.StartCoroutine(crop.Harvest());
            }
        }
    }
}

using DG.Tweening;
using UnityEngine;
using YG;

public class SaveGame : MonoBehaviour
{
    public GameObject[] BuyPlayces;
    public GameObject[] Stands;
    public GuiGame Game;

    void Start()
    {
        if (YandexGame.savesData.level == 0)
        {
            Game.level = 1;
            Game.money = 150;
            Game.maxProgress = 100;
            Game.maxWeight = 10;
            Game.levelUpMoney = 60;

        }
        else
        {
            Game.money = YandexGame.savesData.money;
            Game.money += YandexGame.savesData.countTable;
            YandexGame.savesData.countTable = 0;
            Game.level = YandexGame.savesData.level;
            Game.levelUpMoney = YandexGame.savesData.levelUpMoney;
            Game.countProgress = YandexGame.savesData.progress;
            Game.maxWeight = YandexGame.savesData.weight;
            Game.maxProgress = YandexGame.savesData.maxProgress;
        }


        if (YandexGame.savesData.countPlaces != null)
        {
            for (int i = 0; i < BuyPlayces.Length; i++)
            {
                BuyPlayces[i].GetComponent<BuyPlace>().count = YandexGame.savesData.countPlaces[i];
            }
        }
        if (YandexGame.savesData.countStands != null)
        {
            for (int i = 0; i < Stands.Length; i++)
            {
                Stands[i].GetComponent<Stand>().count = YandexGame.savesData.countStands[i];
                if(Stands[i].GetComponent<Stand>().count > 0)
        {
            for(int j = 0; j < Stands[i].GetComponent<Stand>().count; j++)
            {
                GameObject c = Instantiate(Stands[i].GetComponent<Stand>().objType, transform.position, Quaternion.identity);
                c.transform.DOJump(Stands[i].GetComponent<Stand>().places[Stands[i].GetComponent<Stand>().index].position, 5f, 1, 0.2f);
                //c.transform.rotation = Quaternion.Euler(0, 0, 0);
                c.transform.parent = Stands[i].GetComponent<Stand>().products.transform;
                //c.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                Stands[i].GetComponent<Stand>().index++;
                if (Stands[i].GetComponent<Stand>().index == 12) Stands[i].GetComponent<Stand>().index = 0;
            }
        }
            }
        }
    }

    public void Save()
    {
        YandexGame.savesData.money = Game.money;
        YandexGame.savesData.level = Game.level;
        YandexGame.savesData.levelUpMoney = Game.levelUpMoney;
        YandexGame.savesData.progress = Game.countProgress;
        YandexGame.savesData.weight = Game.maxWeight;
        YandexGame.savesData.maxProgress = Game.maxProgress;
        YandexGame.savesData.countPlaces = new int[BuyPlayces.Length];
        YandexGame.savesData.countStands = new int[Stands.Length];
        for (int i = 0; i < BuyPlayces.Length; i++)
        {
            YandexGame.savesData.countPlaces[i] = BuyPlayces[i].GetComponent<BuyPlace>().count;
        }
        for (int i = 0; i < Stands.Length; i++)
        {
            YandexGame.savesData.countStands[i] = Stands[i].GetComponent<Stand>().count;
        }
        YandexGame.SaveProgress();

    }

    public void ResetGame()
    {
        YandexGame.savesData.level = 1;
        YandexGame.savesData.money = 150;
        YandexGame.savesData.maxProgress = 100;
        YandexGame.savesData.weight = 10;
        YandexGame.savesData.levelUpMoney = 60;
        YandexGame.savesData.progress = 0;
        for (int i = 0; i < BuyPlayces.Length; i++)
        {
            YandexGame.savesData.countPlaces[i] = 0;
        }
        for (int i = 0; i < Stands.Length; i++)
        {
            YandexGame.savesData.countStands[i] = 0;
        }
        YandexGame.SaveProgress();
    }


}

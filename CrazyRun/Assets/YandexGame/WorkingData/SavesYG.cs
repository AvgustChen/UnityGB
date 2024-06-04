
using UnityEngine;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)


        // Ваши сохранения
        public int leaderBoard;
        public int money;
        public int countTable;
        public int level;
        public int levelUpMoney;
        public float progress;
        public float maxProgress;
        public int weight;
        public int[] countPlaces;
        public int[] countStands;

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений

    }
}

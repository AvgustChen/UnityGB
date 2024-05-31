using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Panels : MonoBehaviour
{
    [SerializeField] TMP_Text countText;
    public bool startPanel;
    public AudioSource fonMusic;

    void Start()
    {
        Time.timeScale = 0;


        if (!startPanel)
        {
            countText.text = GameData.count.ToString();
                YandexGame.savesData.maxCount += GameData.count;
                YandexGame.SaveProgress();
                YandexGame.NewLeaderboardScores("Leaders", YandexGame.savesData.maxCount);

        }
        fonMusic.Pause();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadRestart()
    {
        SceneManager.LoadScene(1);
    }

    public void Close()
    {
        fonMusic.Play();
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}

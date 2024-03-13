using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{

    public void LoadLeve1()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLeve2()
    {
        SceneManager.LoadScene(1);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class OpenNextIsland : MonoBehaviour
{
    [SerializeField] List<GameObject> obj;
    [SerializeField] GameObject WinPanel;
   public bool finish;


    void Update()
    {
        if (!finish)
        {
            if (obj != null)
            {
                foreach (var item in obj)
                {
                    if (item == null)
                    {
                        obj.Remove(item);
                        break;
                    }
                }
            }
            if (obj.Count == 0)
            {
                transform.parent.GetComponent<BoxCollider>().enabled = false;
                Destroy(gameObject);
            }
        }
        else
        {
            if (obj.Count == 0)
            {
                transform.parent.GetComponent<BoxCollider>().enabled = false;
                Destroy(gameObject);
                Time.timeScale = 0;
                WinPanel.SetActive(true);
            }
        }
    }
}

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class Helper : MonoBehaviour
{
    public Transform farms;
    public Transform stands;
    public Transform bag;
    Animator anim;
    public NavMeshAgent _agent;
    bool canMove;
    Vector3 startPosition;
    public float timer;
    float maxtimer;
    public Image image;
    public GameObject buyHelper;

    void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.enabled = true;
        maxtimer = 180;
        timer = 180;
    }
    void Start()
    {

        startPosition = transform.position;
        canMove = true;
        _agent = GetComponent<NavMeshAgent>();
        _agent.enabled = true;
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        timer -= Time.deltaTime;
        image.fillAmount = timer / maxtimer;
        if (timer < 0)
        {
            Destroy(gameObject);
            // _agent.isStopped = true;
            // _agent.enabled = false;
            // transform.position = startPosition;
            // buyHelper.GetComponent<BuyPlace>().count = 0;
            // gameObject.SetActive(false);
            // buyHelper.SetActive(true);
        }
        if (_agent.enabled == true)
        {
            if (_agent.remainingDistance < 0.8f)
            {
                anim.SetBool("Run", false);
                if (canMove)
                {
                    if (bag.transform.childCount > 0) // Относим на стенд
                    {
                        foreach (Transform item in stands.transform)
                        {
                            if (bag.transform.GetChild(0).tag == item.GetComponent<Stand>().objType.tag)
                            {
                                anim.SetBool("Run", true);
                                _agent.SetDestination(item.position);
                                break;
                            }
                        }
                    }
                    else
                    {
                        int rand = Random.Range(1, 3);
                        if (rand == 1) // Идем поливать
                        {
                            rand = Random.Range(0, farms.transform.childCount);
                            if (farms.transform.GetChild(rand).gameObject.activeInHierarchy)
                            {
                                if (farms.transform.GetChild(rand).gameObject.transform.GetChild(0).GetComponent<Crops>().grow == 0)
                                {
                                    anim.SetBool("Run", true);
                                    _agent.SetDestination(farms.transform.GetChild(rand).gameObject.transform.GetChild(1).position);
                                }
                                else if (farms.transform.GetChild(rand).gameObject.transform.GetChild(0).GetComponent<Crops>().grow >= 1)
                                {
                                    anim.SetBool("Run", true);
                                    _agent.SetDestination(farms.transform.GetChild(rand).gameObject.transform.GetChild(1).position);
                                }
                            }
                        }

                        else if (rand == 2) // Идем собирать урожай
                        {
                            rand = Random.Range(0, farms.transform.childCount);
                            if (farms.transform.GetChild(rand).gameObject.transform.GetChild(3).transform.childCount > 0)
                            {
                                Debug.Log("Собирать урожай");
                                anim.SetBool("Run", true);
                                _agent.SetDestination(farms.transform.GetChild(rand).gameObject.transform.GetChild(3).position);
                            }
                        }
                    }
                }
            }
        }


    }

    void CanMove()
    {
        canMove = false;
    }
    void CanMoveTrue()
    {
        canMove = true;
    }


}

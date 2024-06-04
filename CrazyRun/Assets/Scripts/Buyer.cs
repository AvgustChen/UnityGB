using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class Buyer : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    public NavMeshAgent _agent;
    Transform[] waypoints;
    Transform[] walkPoints;
    public Image icon;
    public Sprite sad;
    public Sprite happy;
    public GameObject products;
    public GameObject wishProduct;

    Transform buyPoint;
    public Transform GoAway;
    public bool buy;
    public bool changeProduct;

    public int _CurrentWaypointIndex;
    GameObject stands;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stands = GameObject.Find("Stands");
        waypoints = transform.parent.GetComponent<WayPointsBuyer>().waypoints;
        walkPoints = transform.parent.GetComponent<WayPointsBuyer>().walkPoints;
        GoAway = transform.parent.GetComponent<WayPointsBuyer>().GoAway;
        buyPoint = transform.parent.GetComponent<WayPointsBuyer>().buyPoint;
        _agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        _CurrentWaypointIndex = Random.Range(0, walkPoints.Length);
        _agent.SetDestination(walkPoints[_CurrentWaypointIndex].position);

    }

    void Update()
    {
        if (_agent.remainingDistance < 0.5f)
        {
            if (!buy)
            {
                if (!changeProduct)
                {
                    _CurrentWaypointIndex = Random.Range(0, waypoints.Length);
                    if (!waypoints[_CurrentWaypointIndex].gameObject.activeInHierarchy)
                    {
                        _CurrentWaypointIndex = Random.Range(0, walkPoints.Length);
                        _agent.SetDestination(walkPoints[_CurrentWaypointIndex].position);
                    }
                    else
                    {
                        changeProduct = true;
                        var stand = waypoints[_CurrentWaypointIndex].gameObject.GetComponent<Stand>();
                        icon.sprite = stand.image.sprite;
                        wishProduct = stand.objType;
                        _agent.SetDestination(waypoints[_CurrentWaypointIndex].transform.position);
                        

                    }
                }
                else
                {
                    if (products.transform.childCount == 0)
                    {
                        _agent.SetDestination(GoAway.position); // Обиделся и ушел
                         icon.sprite = sad;
                    }
                    else
                    {
                        _agent.SetDestination(buyPoint.position); // довольный пошел на кассу
                        // waypoints[_CurrentWaypointIndex].gameObject.GetComponent<Stand>().count -= 1;
                        icon.sprite = happy;
                    }
                }

            }
            else
            {
                _agent.SetDestination(GoAway.position); // с покупкой ушел в лес
            }

        }
        if(rb.velocity.magnitude <= 0.5f )
        {
            anim.SetBool("Run", true);
        }
        else if(rb.velocity.magnitude == 0f)
        {
            anim.SetBool("Run", false);
        }
    }
}
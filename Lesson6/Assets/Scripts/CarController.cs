using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Unity.VisualScripting;

[RequireComponent(typeof(NavMeshAgent))]
public class CarController : MonoBehaviour
{
    [SerializeField] Text timer;
    [SerializeField] Text circles;
    int laps;
    double timeFirstLap;
    double timeSecondLap;
    double timeThirdLap;
    private NavMeshAgent _agent;
    public Transform[] waypoints;
    public int _CurrentWaypointIndex;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _CurrentWaypointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        circles.text = (Math.Round(Time.time)).ToString();
        timer.text = "Круг: " + laps + "/3\n"
                    + "1 круг: " + timeFirstLap + "\n"
                    + "2 круг: " + timeSecondLap + "\n"
                    + "3 круг: " + timeThirdLap;
        if (laps < 3)
        {
            if (_agent.remainingDistance < _agent.stoppingDistance + 0.25f)
            {
                _CurrentWaypointIndex = (_CurrentWaypointIndex + 1) % waypoints.Length;
                _agent.SetDestination(waypoints[_CurrentWaypointIndex].position);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            if (laps == 0)
            {
                timeFirstLap = Math.Round(Time.time);
                laps++;
            }
            else if (laps == 1)
            {
                timeSecondLap = Math.Round(Time.time) - timeFirstLap;
                laps++;
            }
            else if (laps == 2)
            {
                timeThirdLap = Math.Round(Time.time) - timeFirstLap - timeSecondLap;
                laps++;
            }
        }
    }
}

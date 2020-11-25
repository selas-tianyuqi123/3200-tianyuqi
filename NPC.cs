using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    //Ai find the way
    private NavMeshAgent ai;
    //Target coordinate 
    private Vector3 target;
    void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        //get one coordinate
        target = AIManager.Instance.GetPoint();
        //set ai way find to the coordinate 
        ai.SetDestination(target);

    }

    void Update()
    {
        //if npc find one coordinate, they will automatic find next
        if (Vector3.Distance(transform.position, target) <= 1f)
        {
            target = AIManager.Instance.GetPoint();
            ai.SetDestination(target);
        }
    }
}

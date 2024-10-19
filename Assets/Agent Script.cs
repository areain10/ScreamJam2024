using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour
{
    [SerializeField] Transform target;

    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateUpAxis= false;
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(target.position);
    }
}

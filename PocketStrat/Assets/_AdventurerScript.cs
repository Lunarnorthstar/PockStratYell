using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class _AdventurerScript : MonoBehaviour
{
    public float _Health, _Damage;
    private NavMeshAgent _agent;

    [SerializeField] private GameObject NavmeshAgentFollow;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();   
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(NavmeshAgentFollow.transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private NavMeshAgent _player;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<NavMeshAgent>();
        _player.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeOf(NavMeshAgent))]
public class MoveTo : MonoBehaviour
{
    public Transform goal;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent= GetComponenet<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //if move to key is pressed
        // while current positon doesnt equal goal position
        //agent.destination=goal.position
        //if(Input.GetMouseButtonDown(0))
    }
    private void ClickingMove(){
        Ray ray= Camera.FindWithTag("EditorOnly").ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit= Physics.RayCast(ray, out hit);
        if(hasHit)
            setDest(hit.point);
    }
    private void setDest(Vector3 target){
        agent.SetDestination(target);
    }
}

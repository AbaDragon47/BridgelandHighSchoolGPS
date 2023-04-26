using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeOf(NavMeshAgent))]
public class MoveTo : MonoBehaviour
{
    public Transform goal;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent= GetComponent<NavMeshAgent>();
        Camera.main.enabled=true;
        GameObject.FindWithTag("EditorOnly").GetComponent<Camera>().enabled=false;
        Debug.Log(GameObject.FindWithTag("EditorOnly").GetComponent<Camera>().enabled);
    }

    // Update is called once per frame
    void Update()
    {
        //if move to key is pressed
        // while current positon doesnt equal goal position
        //agent.destination=goal.position
        if(Input.GetKeyDown(KeyCode.Space)){
            
            Camera.main.enabled=false;
            GameObject.FindWithTag("EditorOnly").GetComponent<Camera>().enabled=true;
            ClickingMove();
        }
        else{
            Camera.main.enabled=true;
            GameObject.FindWithTag("EditorOnly").GetComponent<Camera>().enabled=false;
        }
    }
    private void ClickingMove(){
        Ray ray= GameObject.FindWithTag("EditorOnly").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit= Physics.Raycast(ray, out hit);
        if(hasHit)
            setDest(hit.point);
    }
    private void setDest(Vector3 target){
        agent.SetDestination(target);
    }
}

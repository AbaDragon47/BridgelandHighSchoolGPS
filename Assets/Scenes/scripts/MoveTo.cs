using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeOf(NavMeshAgent))]
public class MoveTo : MonoBehaviour
{
    public Transform goal;
    public NavMeshAgent agent;
    private Camera cam;
    private bool hasHit;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        cam= Camera.main;
        agent= GetComponent<NavMeshAgent>();
        Camera.main.enabled=true;
        GameObject.FindWithTag("EditorOnly").GetComponent<Camera>().enabled=false;
        //Debug.Log(GameObject.FindWithTag("EditorOnly").GetComponent<Camera>().enabled);
    }

    // Update is called once per frame
    void Update()
    {
        //if move to key is pressed
        // while current positon doesnt equal goal position
        //agent.destination=goal.position
        Debug.Log(Input.GetKey(KeyCode.Space)?"ye":"no");
        if(Input.GetKey(KeyCode.Space)){
            cam.enabled=false;
            GameObject.FindWithTag("EditorOnly").GetComponent<Camera>().enabled=true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("started clickMove");
            ClickingMove();
        }
        else if(!Input.GetKey(KeyCode.Space)){
            Debug.Log("space stopped");
            GameObject.FindWithTag("EditorOnly").GetComponent<Camera>().enabled=false;
            cam.enabled=true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        
        
    }
    private void ClickingMove(){
        //bool hasHap=false; 
        Debug.Log("attempting to get pos");
        Ray ray= GameObject.FindWithTag("EditorOnly").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        bool hasHit= Physics.Raycast(ray, out hit);
        Debug.Log(hasHit?"got pos!":"failed");
        if(hasHit){
            setDest(hit.point);
            Debug.Log("where cube is: "+agent.transform.position+" goal: "+hit.point);  
        }
         Debug.Log("ended clicking");   
    }
    private void setDest(Vector3 target){
        agent.SetDestination(target);
    }
}

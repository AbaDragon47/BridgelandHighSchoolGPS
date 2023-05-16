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
    private Camera navCam;
    private bool hasHit;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        navCam= GameObject.FindWithTag("EditorOnly").GetComponent<Camera>();
        cam= Camera.main;
        agent= GetComponent<NavMeshAgent>();
        Camera.main.enabled=true;
        navCam.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        //if move to key is pressed
        // while current positon doesnt equal goal position
        //agent.destination=goal.position
        //Debug.Log(Input.GetKey(KeyCode.Space)?"ye":"no");
        if(Input.GetKey(KeyCode.Space)){  
            cam.enabled=false;
            GameObject.FindWithTag("EditorOnly").GetComponent<Camera>().enabled=true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            camMove();
            ClickingMove();
        }
        else if(Input.GetKeyUp(KeyCode.Space)){
            GameObject.FindWithTag("EditorOnly").GetComponent<Camera>().enabled=false;
            cam.enabled=true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        
        
    }
    private void ClickingMove(){
        Ray ray= GameObject.FindWithTag("EditorOnly").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        bool hasHit= Physics.Raycast(ray, out hit);
        Debug.Log(hasHit?"got pos!":"failed");
        if(hasHit){
            setDest(hit.point);
            Debug.Log("where cube is: "+agent.transform.position+" goal: "+hit.point);  
        }
    }
    private void setDest(Vector3 target){
        agent.SetDestination(target);
    }
    private void camMove(){
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");
        float zMove= Input.GetKey(KeyCode.Q)? 0.5f:Input.GetKey(KeyCode.E)?-0.5f:0f;
        //gameObject.transform.position -= new Vector3(xMove*10f*Time.deltaTime,0, yMove*10f*Time.deltaTime);
        //transform.Translate(new Vector3(xMove * 10f * Time.deltaTime, 0, yMove * 10f * Time.deltaTime));
        //moveInput = new Vector3(xMove*Time.deltaTime , zMove*Time.deltaTime, yMove*Time.deltaTime);
        float speed= 5f;
        navCam.transform.position= navCam.transform.position+ new Vector3(xMove*speed*Time.deltaTime , zMove*speed*Time.deltaTime, yMove*speed*Time.deltaTime);
    }
}

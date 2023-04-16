using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject school;
    private Camera cam;
    private Vector3 moveInput;
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    [SerializeField] private float sens;
    [SerializeField] private float ScrollSpeed;

    float mouseX, mouseY, yRot, xRot;
    Rigidbody rb;
    void Start() {
        rb=GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        cam.transform.position= transform.position+ new Vector3(0,1,0);
    }
    void Update(){
        Move();
        MyMouse();
    }

    private void LateUpdate()
    {
        //called after all physics changes
        //Move();
        rb.AddForce(moveInput*sens,ForceMode.VelocityChange);
        //gameObject.GetComponent<Rigidbody>().position+= moveInput*(sens);
        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        if (cam.orthographic)
            cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        else
            cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        //follow player
        cam.transform.position= transform.position+ new Vector3(0,1,0);

        //maybe i should lock rotation so that i doesnt go through player object
    }
    void Move()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");
        //gameObject.transform.position -= new Vector3(xMove*10f*Time.deltaTime,0, yMove*10f*Time.deltaTime);
        //transform.Translate(new Vector3(xMove * 10f * Time.deltaTime, 0, yMove * 10f * Time.deltaTime));
        moveInput = new Vector3(xMove*transform.forward , 0, yMove*transform.right );


    }
    void MyMouse()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        Debug.Log("x " + mouseX + "y " + mouseY);

        yRot += mouseX * .1f * sensX;
        xRot -= mouseY * .1f * sensY;

        //xRot = Mathf.Clamp(xRot, -90f, 90f);
       // yRot = Mathf.Clamp(yRot, 0f, 180f);

    }

    // Update is called once per frame

}

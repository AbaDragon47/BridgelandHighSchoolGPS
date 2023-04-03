using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject school;
    private Camera cam;
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    [SerializeField] private float ScrollSpeed;

    float mouseX, mouseY, yRot, xRot;
    
    void Start() {

        cam = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        //called after all physics changes
        MyMouse();
        Move();
        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        if (cam.orthographic)
            cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        else
            cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        //transform.rotation = Quaternion.Euler(0, yRot, 0);
    }
    void Move()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");
        transform.position -= new Vector3(xMove*10f*Time.deltaTime,0, yMove*10f*Time.deltaTime);

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

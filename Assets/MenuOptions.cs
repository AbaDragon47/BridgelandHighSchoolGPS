using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour
{
    public Button login;
    public Image menu;
    public Text messageBox;
    bool alrDown;
    private Vector3 currPos;
    void Start()
    {
        login = login.GetComponent<Button>();
        login.onClick.AddListener(TaskOnClick);
        alrDown = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        currPos = menu.rectTransform.position;
        // loginPressed = false;
    }

    private void TaskOnClick()
    {
        Debug.Log("You clicked the button");
        SlideUp();
        messageBox.text = "You logged in!";
        //make canvas go up
        //send message that you logged in!
         
    }
    public void SlideUp()
    {
        menu.rectTransform.localPosition=new Vector3(0, 450,0);
        alrDown = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void SlideDown()
    {
        menu.rectTransform.position=currPos;
        alrDown = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !alrDown)
        {
            //make canvas go down
            Debug.Log("menu going down");
            SlideDown();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && alrDown)
        {
            //make canvas go up
            Debug.Log("menu going up");
            SlideUp();
        }

 
        
    }
}

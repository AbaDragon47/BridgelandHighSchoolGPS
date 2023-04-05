using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject LoginUI;
    public static bool currLoggin = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currLoggin)
                Activate();
            else
                DeActivate();
        }
        
    }

    private void DeActivate()
    {
        throw new NotImplementedException();
    }

    private void Activate()
    {
        throw new NotImplementedException();
    }
}

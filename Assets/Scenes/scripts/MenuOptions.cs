using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using TMPro;
using UnityEditor;

public class MenuOptions : MonoBehaviour
{
    public Button login;
    public Image menu;
    public Text messageBox;
    bool alrDown;
    private Vector3 currPos;

    public TextMeshProUGUI username;
    public TextMeshProUGUI password;
    void Start()
    {
        string connection = "URI=file:" + Application.dataPath + "/login";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        IDbCommand dbcmd;
        dbcmd = dbcon.CreateCommand();

        string q_createTable = "CREATE TABLE IF NOT EXISTS login (username TEXT, password TEXT, salt TEXT)";

        dbcmd.CommandText = q_createTable; 
        dbcmd.ExecuteReader();


        login = login.GetComponent<Button>();
        login.onClick.AddListener(TaskOnClick);
        alrDown = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        currPos = menu.rectTransform.position;
        // loginPressed = false;
        dbcon.Close();

    }
    public static string EncodePasswordToBase64(string password)
    {
        try
        {
            byte[] encData_byte = new byte[password.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        catch (Exception ex)
        {
            throw new Exception("Error in base64Encode" + ex.Message);
        }
    }
    //this function Convert to Decord your Password
    public string DecodeFrom64(string encodedData)
    {
        System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
        System.Text.Decoder utf8Decode = encoder.GetDecoder();
        byte[] todecode_byte = Convert.FromBase64String(encodedData);
        int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        char[] decoded_char = new char[charCount];
        utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        string result = new String(decoded_char);
        return result;
    }

    private void TaskOnClick()
    {
       /* Debug.Log(username.text);
        Debug.Log(password.text);

        Debug.Log("You clicked the button");*/


        string hash = EncodePasswordToBase64(password.text);

        string connection = "URI=file:" + Application.dataPath + "/login";


        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        IDbCommand dbcmd;
        dbcmd = dbcon.CreateCommand();
        dbcmd.CommandText = "drop table if exists classes";
        dbcmd.ExecuteNonQuery();

        string q_createTable = "CREATE TABLE IF NOT EXISTS login (username TEXT PRIMARY KEY, password TEXT)";
        dbcmd.CommandText = q_createTable;
        dbcmd.ExecuteReader();

        IDbCommand cmnd2 = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM login WHERE username = " + "'" + username.text + "'";
        //Debug.Log(query);
        cmnd2.CommandText = query;
        reader = cmnd2.ExecuteReader();
        if (reader.Read())
        {
            if (password.text.Equals(DecodeFrom64(reader[1].ToString())))
            {
                EditorUtility.DisplayDialog("Password Alert", "Password Validated", "Ok");
                //Debug.Log("Password Validated!");
                    
                SlideUp();
            }
            else
            {
                EditorUtility.DisplayDialog("Password Alert", "Password Denied", "Ok");
                //Debug.Log("Password denied");
            }
        }
        else
        {
            IDbCommand cmnd = dbcon.CreateCommand();
            cmnd.CommandText = "INSERT INTO login (username, password) VALUES ('" + username.text + "', '" + hash + "')";
            cmnd.ExecuteNonQuery();
            EditorUtility.DisplayDialog("Password Alert", "Password Created", "Ok");
            //Debug.Log("Password Inserted");

            SlideUp();
        }


        //make canvas go up
        //send message that you logged in!

    }
    public void SlideUp()
    {
        menu.rectTransform.localPosition=new Vector3(0, 1000,0);
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

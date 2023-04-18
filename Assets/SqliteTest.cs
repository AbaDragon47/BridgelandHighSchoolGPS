using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqliteTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string connection = "URI=file:" + Application.dataPath + "/classes";


        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        IDbCommand dbcmd;
        dbcmd = dbcon.CreateCommand();
        dbcmd.CommandText = "drop table if exists classes";
        dbcmd.ExecuteNonQuery();

        string q_createTable = "CREATE TABLE IF NOT EXISTS classes (id INTEGER PRIMARY KEY, room TEXT, teacher TEXT, x1 INTEGER, x2 INTEGER, y1 INTEGER, y2 INTEGER, club TEXT, day TEXT, time TEXT)";

        dbcmd.CommandText = q_createTable;
        dbcmd.ExecuteReader();
        StreamReader strReader = new StreamReader("G:/CODING/LASTCS4proj/Assets/classes.csv");
        bool endOfFile = false;
        int lineNumber = 0;
        while (!endOfFile)
        {
            string data_string = strReader.ReadLine();
            if(data_string == null)
            {
                endOfFile = true;
                break;
            }
            var fields = data_string.Split(',');

            lineNumber++;
            int id = lineNumber;
            string club = fields[0];
            string date = fields[1];
            string time = fields[2];
            string room = fields[3];
            string teacher = fields[4];
            Debug.Log(id.ToString() + ", " + club + ", " + date + ", " + time + ", " + room + ", " + teacher);
            IDbCommand cmnd2 = dbcon.CreateCommand();
            string querynamedthis = "INSERT INTO classes (id, room, teacher, x1, x2, y1, y2, club, day, time) VALUES (" + "'" + id.ToString() + "','" + room + "','" + teacher + "'," + 0 + "," + 0 + "," + 0 + "," + 0 + ",'" + club + "','" + date + "','" + time + "')";
            cmnd2.CommandText = querynamedthis;
            cmnd2.ExecuteNonQuery();
            Debug.Log("Executed successfully");
        }

        // IDbCommand cmnd = dbcon.CreateCommand();
        // cmnd.CommandText = "INSERT INTO classes (id, room, teacher, x1, x2, y1, y2, club) VALUES (0, '3114', 'Mr Garcia', 314, 320, 416, 425, 'CS Club')";
        // cmnd.ExecuteNonQuery();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader; 
        string query = "SELECT * FROM classes";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader(); 
        
        /* while (reader.Read())
        {
            Debug.Log("id: " + reader[0].ToString());
            Debug.Log("room: " + reader[1].ToString());
            Debug.Log("teacher: " + reader[2].ToString());
            Debug.Log("x1: " + reader[3].ToString());   
            Debug.Log("x2: " + reader[4].ToString());
            Debug.Log("y1: " + reader[5].ToString());
            Debug.Log("y2: " + reader[6].ToString());
        }
        */

        dbcon.Close();
    }
    string getRoomInfo(string room)
    {
        string connection = "URI=file:" + Application.dataPath + "/classes";


        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM classes WHERE room=" + "'" + room + "'";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        string output = "";
        while (reader.Read())
        {
            output += reader[2].ToString() + " ";
            output += reader[7].ToString() + " ";
            output += reader[8].ToString() + " ";
            output += reader[9].ToString() + " ";
        }

        dbcon.Close();
        return output;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

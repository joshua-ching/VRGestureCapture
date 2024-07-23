using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVWriter : MonoBehaviour
{

    string filename = "";
    // Start is called before the first frame update
    void Start()
    {

        // filename = Application.dataPath + "/test.csv";

        // TextWriter tw = new StreamWriter(filename, false);
        // tw.WriteLine("Time");
        // tw.Close();

        // tw = new StreamWriter(filename, true);//true cause want to append not create new file
        // tw.WriteLine("this is a test");
        // tw.WriteLine("this is another test test, weee");

        // tw.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingHistory : MonoBehaviour
{

    public InputData inp;

    public List<Vector3> positionListR = new List<Vector3>();

    public List<bool> gripListR = new List<bool>();

    public List<bool> grabListR = new List<bool>();

    public int MAX_SAMPLE_SIZE;

    public float sampleRate = .5f;

    float waitUntil;


    public string ListToString<T>(List<T> myList){
        string returnString ="";
        
        for (int i = 0; i < myList.Count; i++)
        {
            returnString = returnString + myList[i] + ",";
        }
        return returnString;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Time.time > waitUntil){
            positionListR.Add(inp.posR);
            gripListR.Add(inp.gripR);
            grabListR.Add(inp.grabR);

            waitUntil += sampleRate;

            if(positionListR.Count > MAX_SAMPLE_SIZE){
                positionListR.RemoveAt(0);
                gripListR.RemoveAt(0);
                grabListR.RemoveAt(0);
            }
        }
    }
}
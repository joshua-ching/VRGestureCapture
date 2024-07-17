using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject callibrationTarget;

    public InputData inp;

    public RaycastTargetCalibrator cal;

    public Transform sphere;

    public Transform handControllerRight;//don't use inp cause it returns real world position not in game position

    

    // int calibrationRound = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!calibrating && inp.gripL){
            StartCoroutine(Calibrate());
        }
    }

    Vector3[] calibrationPositionArray = 
    {new Vector3(18,2.5f,-50), 
    new Vector3(18,4,-50), 
    new Vector3(18,1,-50),
    new Vector3(18,2.5f,-46), 
    new Vector3(18,2.5f,-54), 
    new Vector3(18,4,-46),
    new Vector3(18,4f,-54), 
    new Vector3(18,1,-54), 
    new Vector3(18,1,-46)};

    // {new Vector3(18,2.5f,-50), 
    // new Vector3(18,4,-50), 
    // new Vector3(18,1,-50)};
    
    
    
    
    
    
    
    //fill till 8

    bool calibrating;

    // int calibrationLocation;
    IEnumerator Calibrate(){

        calibrating = true;

        for (int i = 0; i < calibrationPositionArray.Length; i++)
        {
            callibrationTarget.transform.position = calibrationPositionArray[i];
            yield return new WaitForSeconds(3);
            cal.Fire();

            // Debug.Log(sphere.position.z);
            // Debug.Log(inp.posR.z);
            Debug.Log( sphere.position.z-handControllerRight.position.z);

            Debug.Log("x is above y is below");
            Debug.Log(sphere.position.y - handControllerRight.position.y);

            yield return new WaitForSeconds(1);

            //(Update scoreboard with data?)
            
        }

        //     break;// once reach 8, set calibrationround to 0 and break from loop
        // }


       
        // StartCoroutine(StartCalibration());
                    // calibrationRound++;

        calibrating = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject callibrationTarget;

    public InputData inp;

    public RaycastTargetCalibrator cal;

    int calibrationRound = 0;
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

    Vector3[] calibrationPositionArray = {new Vector3(0,2,1), new Vector3(0,2,3)};//fill till 8

    bool calibrating;

    // int calibrationLocation;
    IEnumerator Calibrate(){

        calibrating = true;

        for (int i = 0; i < calibrationPositionArray.Length; i++)
        {
            callibrationTarget.transform.position = calibrationPositionArray[calibrationRound];
             yield return new WaitForSeconds(1);
            cal.Fire();
            //(Update scoreboard with data?)
            calibrationRound++;
        }

        //     break;// once reach 8, set calibrationround to 0 and break from loop
        // }


       
        // StartCoroutine(StartCalibration());

        calibrating = false;
    }
}

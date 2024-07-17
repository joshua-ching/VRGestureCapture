using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject callibrationObject;

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
        
    }

    Vector3[] calibrationPosition = {new Vector3(0,2,1), new Vector3(0,2,3)};//fill till 8

    IEnumerator StartCalibration(){

        switch(calibrationRound){
            case 0:
                var startPosition = 1;
                callibrationObject.transform.position = calibrationPosition[startPosition];
            break;

            case 1:

            break;

            case 2:

            break;

            case 3:

            break;

            case 4:

            break;// once reach 8, set calibrationround to 0 and break from loop
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(cal.Fire());
        //(Update scoreboard with data?)

        calibrationRound++;
        StartCoroutine(StartCalibration());

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public GameObject callibrationTarget;

    public GameObject target;

    public InputData inp;

    public RaycastTargetCalibrator cal;

    public Transform sphere;

    public Transform handControllerRight;//don't use inp cause it returns real world position not in game position

    public int hits;

    public int misses;



    // int calibrationRound = 0;
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(Test());
        // StartCoroutine(StartSpawning(2));

    }
    bool spawning = false;

    // Update is called once per frame
    void Update()
    {
        if (!calibrating && inp.gripL)
        {
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


    float MAX_Z = -60;
    float MIN_Z = -40;

    float MAX_Y = 4.2f;
    float MIN_Y = 0.3f;





    void Spawn()
    {
        Instantiate(target, GetSpawnLocation(), Quaternion.identity);
    }


    Vector3 GetSpawnLocation()
    {
        return new Vector3(18, Random.Range(MIN_Y, MAX_Y), Random.Range(MIN_Z, MAX_Z));
    }

    public IEnumerator StartSpawning(float timeBetweenSpawns)
    {
        if (spawning == false)
        {
            spawning = true;
            yield return new WaitForSeconds(3);
            for (int i = 0; i < 20; i++)
            {
                Spawn();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
            spawning = false;
        }

    }

    //fill till 8

    bool calibrating;

    // int calibrationLocation;
    IEnumerator Calibrate()
    {

        calibrating = true;

        for (int i = 0; i < calibrationPositionArray.Length; i++)
        {
            callibrationTarget.transform.position = calibrationPositionArray[i];
            yield return new WaitForSeconds(3);
            cal.Fire();

            // Debug.Log(sphere.position.z);
            // Debug.Log(inp.posR.z);
            Debug.Log(sphere.position.z - handControllerRight.position.z);

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

    IEnumerator Test()
    {
        yield return new WaitForSeconds(.2f);
        cal.MoveTargetSphereAbsolute(new Vector3(0, 0, .01f));
        StartCoroutine(Test());
    }
}

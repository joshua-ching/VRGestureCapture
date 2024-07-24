using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class GameManager : MonoBehaviour
{

    public GameObject callibrationTarget;

    public GameObject target;

    public InputData inp;

    public RaycastTargetCalibrator cal;

    public Transform sphere;

    public Transform handControllerRight;//don't use inp cause it returns real world position not in game position

    public int hits;

    public int presses;

    public int misses;

    public int selectionType = 0;//must be changed in inspector or something cause its public

    public TextMeshProUGUI scoreboard;

    public TextMeshProUGUI timeLog;

    public TextMeshProUGUI title;

    public TextMeshProUGUI timerText;

    // public CSVWriter csvWriter;

    bool triggerDown = false;

    string filename = "";

    public void Test2(){
        Debug.Log("connection maed");
    }


    // int calibrationRound = 0;
    // Start is called before the first frame update
    void Start()
    {

        //csv stuff
        filename = Application.dataPath + "/test.csv";


        TextWriter tw = new StreamWriter(filename, false);
        tw.WriteLine("Time,Size,Missed");
        tw.Close();

        // tw = new StreamWriter(filename, true);//true cause want to append not create new file
        // tw.WriteLine("this is a test");
        // tw.WriteLine("this is another test test, weee");

        // tw.Close();

    }

    public void WriteTimeToCSV(string data){
        TextWriter tw = new StreamWriter(filename, true);//true cause want to append not create new file
        tw.WriteLine(data);
        tw.Close();
    }

    int level = 1;





    bool spawning = false;

    public XRInteractorLineVisual xrLine;

    bool isTiming;

    public GameObject playerSpawn;

    public RecenterOrigin ro;
    // Update is called once per frame
    void Update()
    {

        if(isTiming){
            timerText.text = (Time.time - startTime).ToString();
        }else{
            
        }
        misses = presses - hits;
        scoreboard.text = hits.ToString() + "   " + presses.ToString() + "  " + misses.ToString();


        if (!calibrating && inp.gripL)
        {
            StartCoroutine(Calibrate());
        }



        if(inp.triggerR && !triggerDown){
            triggerDown=true;
            presses++;
        }else if(!inp.triggerR){
            triggerDown = false;
        }

        if(selectionType == 0){
            xrLine.enabled= true;
        }else{
            xrLine.enabled= false;
        }


        if(inp.AButtonL && !spawning){
            switch(level){
                case 1:
                playerSpawn.transform.position = new Vector3(5, 4, -50);
                break;

                case 2:
                playerSpawn.transform.position = new Vector3(-5, 4, -50);
                break;

                case 3:
                playerSpawn.transform.position = new Vector3(-20, 4, -50);
                break;
            }
            
                
            ro.Recenter();

            StartCoroutine(StartSpawning(2));






            level++;

            if(level == 4){
                level = 1;
                selectionType = 1;
            }
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


    float MAX_Z = -56;
    float MIN_Z = -43;

    float MAX_Y = 4.2f;
    float MIN_Y = 0.3f;



    GameObject spawnedObject;

    TargetBehavior Spawn()
    {
        spawnedObject = Instantiate(target, GetSpawnLocation(), Quaternion.identity);
        return spawnedObject.GetComponent<TargetBehavior>();
    }

    TargetBehavior checkBehavior;


    Vector3 GetSpawnLocation()
    {
        return new Vector3(18, Random.Range(MIN_Y, MAX_Y), Random.Range(MIN_Z, MAX_Z));
    }

    float startTime;

    public IEnumerator StartSpawning(float timeBetweenSpawns)
    {
        
        if (spawning == false)
        {
            spawning = true;

            timeLog.text = "";

            title.text = "Starting in 3";
            yield return new WaitForSeconds(1);
            title.text = "Starting in 2";
            yield return new WaitForSeconds(1);
            title.text = "Starting in 1";
            yield return new WaitForSeconds(1);
            title.text = "potato";

            //timer stuff
            startTime = Time.time;
            isTiming = true;

            //

            
            
            presses = 0;
            hits = 0;

            for (int i = 0; i < 15; i++)
            {
                checkBehavior = Spawn();
                while(!checkBehavior.hasBeenSelected){
                    yield return new WaitForSeconds(0.001f);
                }
                timeLog.text += (Time.time - checkBehavior.startTime) + ", ";
            }
            spawning = false;
            isTiming = false;
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

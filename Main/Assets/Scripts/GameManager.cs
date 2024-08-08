using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.XR.CoreUtils;
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

    public bool lazerSelection = false;//must be changed in inspector or something cause its public

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

    public void WriteToCSV(string data){
        TextWriter tw = new StreamWriter(filename, true);//true cause want to append not create new file
        tw.WriteLine(data);
        tw.Close();
    }

    public int level = 5;


    bool spawning = false;

    public XRInteractorLineVisual xrLine;

    bool isTiming;

    public GameObject playerSpawn;

    public RecenterOrigin ro;

    public GameObject player;

    bool bButtonNotPressed = true;
    // Update is called once per frame
    void Update()
    {
        if(isTiming){
            timerText.text = (Time.time - startTime).ToString();
        }else{
            
        }
        misses = presses - hits;
        scoreboard.text = "Hits: " + hits.ToString() + "\nMisses: " + misses.ToString();


        if (!calibrating && inp.gripL)
        {
            // StartCoroutine(Calibrate());
        }



        if(inp.triggerR && !triggerDown){
            triggerDown=true;
            presses++;
        }else if(!inp.triggerR){
            triggerDown = false;
        }

        if(lazerSelection){
            xrLine.enabled= true;
        }else{
            xrLine.enabled= false;
        }   

        if(inp.BButtonL && bButtonNotPressed){
            lazerSelection = !lazerSelection;
            bButtonNotPressed = false;
        }
        
        if(!inp.BButtonL){
            bButtonNotPressed = true;
        }


        if(inp.AButtonL && !spawning){
            
            if(level == 7){
                level = 1;
                lazerSelection = true;
            }
            
                
            // ro.Recenter();

            if(level <= 3){
                                StartCoroutine(StartSpawning(0.5f));
            }else{
                                StartCoroutine(StartSpawning(0.15f));
            }

            StartCoroutine(SetPosition());
            







            // if(level == 6){
            //     level = 1;
            //     selectionType = 1;
            // }
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

    IEnumerator SetPosition(){
        player.GetComponent<CharacterController>().enabled = false;

        yield return new WaitForSeconds(1);
                        Debug.Log("teliportedran");

         switch(level){
                case 1:
                player.transform.position = new Vector3(5, 4, -50);
                Debug.Log("teliported");
                break;

                case 2:
                player.transform.position = new Vector3(-5, 4, -50);
                Debug.Log("teliported2");

                break;

                case 3:
                player.transform.position = new Vector3(-15, 4, -50);
                break;

                case 4:
                player.transform.position = new Vector3(5, 4, -50);
                break;

                case 5:
                player.transform.position = new Vector3(-5, 4, -50);
                break;

                case 6:
                player.transform.position = new Vector3(-15, 4, -50);
                break;
            }
                    player.GetComponent<CharacterController>().enabled = true;

    }

    TargetBehavior Spawn(float targetSize)
    {
        spawnedObject = Instantiate(target, GetSpawnLocation(), Quaternion.identity);
        spawnedObject.transform.localScale = new Vector3(targetSize, targetSize, targetSize);
        return spawnedObject.GetComponent<TargetBehavior>();
    }

    TargetBehavior checkBehavior;

    Vector3 previousSpawnLocation = Vector3.zero;

    Vector3 currentSpawnLocation;
    Vector3 GetSpawnLocation()
    {
        currentSpawnLocation = new Vector3(18, Random.Range(MIN_Y, MAX_Y), Random.Range(MIN_Z, MAX_Z));

        Debug.Log("previous spawn" + previousSpawnLocation + "current spawn" + currentSpawnLocation + "distence"+Vector3.Distance(currentSpawnLocation,previousSpawnLocation));
        
        if(previousSpawnLocation == Vector3.zero){
            Debug.Log("initializedddddd");
            previousSpawnLocation = currentSpawnLocation;
            return currentSpawnLocation;
        }
        //if not first time running
        //make sure distance from previous spawn is far enough away
        while(Vector3.Distance(currentSpawnLocation,previousSpawnLocation) < 1){
            currentSpawnLocation = new Vector3(18, Random.Range(MIN_Y, MAX_Y), Random.Range(MIN_Z, MAX_Z));
        } 
        previousSpawnLocation = currentSpawnLocation;
        return currentSpawnLocation;
    }

    float startTime;

    public IEnumerator StartSpawning(float targetSize)
    {
        
        if (spawning == false)
        {
            presses = 0;
            hits = 0;
            spawning = true;

            timeLog.text = "";

            title.text = "Starting in 3";
            yield return new WaitForSeconds(1);
            title.text = "Starting in 2";
            yield return new WaitForSeconds(1);
            title.text = "Starting in 1";
            yield return new WaitForSeconds(1);

            title.text = "Level ";
            switch(level){
                case 1:
                title.text += "1/6";
                break;
                case 2: 
                title.text += "2/6";
                break;
                case 3: 
                title.text += "3/6";
                break;
                case 4:
                title.text += "4/6";
                break;
                case 5:
                title.text += "5/6";
                break;
                case 6:
                title.text += "6/6";
                break;
            }

            //timer stuff
            startTime = Time.time;
            isTiming = true;

            //

            
            
            

            for (int i = 0; i < 10; i++)
            {
                checkBehavior = Spawn(targetSize);
                while(!checkBehavior.hasBeenSelected){
                    yield return new WaitForSeconds(0.001f);
                }
                timeLog.text += (Time.time - checkBehavior.startTime) + ", ";
            }
            spawning = false;
            isTiming = false;
            WriteToCSV("End of Level" + level);
            level++;

            if(level == 7)
            {
                round++;

            }
            if (round == 2){
                finishRenderer.material = finishMaterial;
            }

        }

    }

    public MeshRenderer finishRenderer;

    public Material finishMaterial;
    int round = 0;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net.NetworkInformation;
using JetBrains.Annotations;
using UnityEngine.Experimental.AI;
using UnityEditor.Timeline;
using Unity.Mathematics;



// to track the movement or gestures of the hand. then write that information somewhere (like an event?) and other actions will be taken.
// Raw data processor
public class GestureManagement : MonoBehaviour
{

    public TrackingHistory th;
    public Transform originTransform;

    public Transform directionTransform;
    Vector3 direction;


    public InputData inp;

    public ObjectManipulation objManip;

    public float distanceBetweenHands;

    public TextMeshProUGUI debug;

    public TextMeshProUGUI debug2;

    public GameObject cube;
    Vector3 cubepos;


    bool isGrabbing = false;

    bool isRescaling = false;
    Vector3 worldInitialSize;

    Vector3 handInitialPos;

    Quaternion handInitionRotation;

    public GameObject worldsObject;

    bool isControllingVolume;
    // public AudioSource bg;



    //volume gesture

    public void VolumeControlOpen(Collider collider)
    {
        if (collider.gameObject.name == "ear")
        {
            // debug2.text = "ear touch";
            // initialBgLevel = bg.volume;
            isControllingVolume = true;
            handInitialPos = inp.posR;
            // volumeBar.SetActive(true);
        }
        else { }
    }

    public void VolumeControlClose(Collider collider)
    {
        if (collider.gameObject.name == "ear")
        {
            // debug2.text = "ear touch";
            isControllingVolume = false;
            // volumeBar.SetActive(false);
        }
        else { }
    }


    public void VolumeControlChange(Collider collider)
    {
        // if(collider.gameObject.name == "volumebar"){
        if (isControllingVolume)
        {

        }
        // }
    }


    public static float TIME_FOR_QUICK_ACTION = 0.5f;

    bool MenuCheck()
    {

        //check hand movement over .5 seconds
        //find how many slices is .5 seconds
        // this isn't working properly
        int samplesToCheck = (int)(TIME_FOR_QUICK_ACTION / TrackingHistory.sampleRate);

        //perameters for semi straight line. Tweek it for better feeling
        float reqYdist = .3f;
        float maxXdist = .05f;

        float lowY = 0;
        float highY = 0;
        float lowX = 0;
        float highX = 0;

        float startingY = 0;

        // Debug.Log("check samples" + samplesToCheck);
        for (int i = th.posListR.Count - samplesToCheck; i < th.posListR.Count; i++)
        {
            //instantiate values
            if (i == (th.posListR.Count - samplesToCheck))
            {
                lowY = highY = startingY = th.posListR[i].y;
                lowX = highX = th.posListR[i].x;

            }
            //log highest and lowest values
            if (th.posListR[i].y > highY)
            {
                highY = th.posListR[i].y;
            }
            if (th.posListR[i].y < lowY)
            {
                lowY = th.posListR[i].y;
            }
            if (th.posListR[i].y > highX)
            {
                highX = th.posListR[i].x;
            }
            if (th.posListR[i].y > highX)
            {
                highX = th.posListR[i].x;
            }

            if (!th.gripListR[i])
            {
                return false;
            }
        }
        // if within perameters and from down to up
        if ((highY - lowY > reqYdist) && (highX - lowX < maxXdist) && (th.posListR[th.posListR.Count - 1].y - startingY > 0))
        {
            // success logic
            // do the menu open thing
            cube.transform.position += new Vector3(.1f, 0, 0);
            return true;
        }
        return false;
    }

    public WorldHolder wh;

    public Transform closestWorld = null;

    public GameObject player;

    public Material selectMaterial;

    public Material defaultMaterial;
    void SelectWorldCheck()
    {
        for (int i = 1; i < wh.worldList.Length; i++)
        {
            if (closestWorld == null)
            {
                closestWorld = wh.worldList[i];
            }
            else
            {
                //if i world is closer than previously closest world
                if (Mathf.Abs(Vector3.Distance(wh.worldList[i].position, player.transform.position)) < Mathf.Abs(Vector3.Distance(closestWorld.position, player.transform.position)))
                {
                    closestWorld = wh.worldList[i];
                }
                else
                {
                    wh.worldList[i].gameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
                }
            }
        }

        // defaultMaterial = closestWorld.gameObject.GetComponent<MeshRenderer>().material;

        // closestWorld.gameObject.GetComponent<MeshRenderer>().material = selectMaterial;
    }

    [SerializeField] private LayerMask layerMask;

    public GameObject mainCamera;

    public GameObject rightController;

    public GameObject rayCastTarget;


    float initialBgLevel;



    IEnumerator RestoreMaterial(MeshRenderer mesh, Material mat)
    {

        mesh.material = selectMaterial;
        yield return new WaitForSeconds(2);
        mesh.material = mat;
    }

    public GameObject marker;

    public GameObject rightHand;

    public RecenterOrigin ro;

    public GameObject playerSpawn;

    public GameManager gameManager;

    public void Select(GameObject selectedObject)
    {
    
        try
        {
            // selectedObject.GetComponent<MeshRenderer>().material = selectMaterial;
        }
        catch { }

        // try{

        if (selectedObject.name == "Target(Clone)")
        {

            selectedObject.GetComponent<TargetBehavior>().OnSelected();
        }
        // }catch{Debug.Log(selectedObject.name + "nope");}

        //what to do for buttons. yes I'm so sorry for this spaghetti

        switch (selectedObject.name)
        {
            case "lvl1":
                playerSpawn.transform.position = new Vector3(5, 4, -50);
                ro.Recenter();

                StartCoroutine(gameManager.StartSpawning(2));
                return;
            case "lvl2":
                playerSpawn.transform.position = new Vector3(-5, 4, -50);
                ro.Recenter();
                StartCoroutine(gameManager.StartSpawning(1.5f));

                return;
            case "lvl3":
                playerSpawn.transform.position = new Vector3(-15, 4, -50);
                ro.Recenter();
                StartCoroutine(gameManager.StartSpawning(2));

                return;
            case "lvl4":
                playerSpawn.transform.position = new Vector3(-23, 4, -50);
                ro.Recenter();
                StartCoroutine(gameManager.StartSpawning(2));

                return;

        }
    }

    public GameObject selectedObject;

    public int rayDistance = 500;

    public GameObject GetConfirmRayObject(){


        if (Physics.Raycast(rightHand.transform.position, rightHand.transform.forward, out RaycastHit hitInfo2, rayDistance))
            {
                Debug.Log("double checking" + hitInfo2.transform.gameObject.name);
                try
                {
                    
                    return hitInfo2.transform.gameObject;
                }
                catch { }

            };



            return null;
    }
    void Update()
    {



        if (Time.time > 2)
        {
            MenuCheck();
            objManip.RotateObject(worldsObject);
            SelectWorldCheck();
            // worldsObject.transform.Rotate(0, 5 * Time.deltaTime, 0);

        }

        if (isControllingVolume)
        {
            // bg.volume = initialBgLevel + ((5 * (inp.posR.y - handInitialPos.y)));
            // Debug.Log((5 * (inp.posR.y - handInitialPos.y)).ToString());
        }


        if (gameManager.lazerSelection)
        {

            if (Physics.Raycast(rightHand.transform.position, rightHand.transform.forward, out RaycastHit hitInfo2, rayDistance))
            {
                try
                {
                    // hitInfo2.transform.gameObject.GetComponent<MeshRenderer>().material = selectMaterial;
                    marker.transform.position = hitInfo2.point;
                    selectedObject = hitInfo2.transform.gameObject;
                }
                catch { }

            };





        }
        else if (!gameManager.lazerSelection)
        {



            if (Physics.Raycast(mainCamera.transform.position, rayCastTarget.transform.position - mainCamera.transform.position, out RaycastHit hitInfo, rayDistance))
            {
                try
                {
                    // hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material = selectMaterial;
                    Select(hitInfo.transform.gameObject);
                    marker.transform.position = hitInfo.point;
                    // StartCoroutine(RestoreMaterial(hitInfo.transform.gameObject.GetComponent<MeshRenderer>(),hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material));

                }
                catch
                { }

            };



        }



        // uncomment below for lazer select

        // switch raycasting target for hand through head




        // Debug.DrawRay(mainCamera.transform.position, rayCastTarget.transform.position - mainCamera.transform.position, Color.blue, 100f);






        //cube stuff------------

        direction = directionTransform.position - originTransform.position;

        distanceBetweenHands = Vector3.Distance(inp.posL, inp.posR);

        // debug.text = distanceBetweenHands.ToString();


        if (!inp.grabL)
        {
            isRescaling = false;
        }





        if (inp.grabR && inp.grabL)
        {
            objManip.Rescale(closestWorld.gameObject);
        }
        else
        {
            //must have trigger to turn this to false when the cue is done
            objManip.isRescaling = false;
        }









        if (!inp.grabL && inp.grabR)
        {
            if (!isGrabbing)
            {
                cubepos = cube.transform.position;
                handInitialPos = inp.posR;
                isGrabbing = true;
            }

            cube.transform.position = cubepos + 3 * (inp.posR - handInitialPos);
        }
        else
        {
            isGrabbing = false;
        }
    }
}

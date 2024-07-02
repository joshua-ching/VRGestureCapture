using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net.NetworkInformation;
using JetBrains.Annotations;
using UnityEngine.Experimental.AI;
using UnityEditor.Timeline;



// to track the movement or gestures of the hand. then write that information somewhere (like an event?) and other actions will be taken.
// Raw data processor
public class GestureManagement : MonoBehaviour
{

    public TrackingHistory th;
    public Transform originTransform;

    public Transform directionTransform;
    Vector3 direction;


    public InputData inp;

    public Vector3 distanceBetweenHands;

    public TextMeshProUGUI debug;

    public TextMeshProUGUI debug2;

    public GameObject cube;

    // public Rigidbody player;

    Vector3 cubepos;


    bool isGrabbing = false;

    bool isRescaling = false;
    Vector3 initialDistenceBetweenHands;

    Vector3 worldInitialSize;

    Vector3 handInitialPos;

    Quaternion handInitionRotation;

    public GameObject worldsObject;


    // public GameObject leftHand;

    // public GameObject volumeBar;

    // public GameObject focusHand;

    bool isControllingVolume;






    //volume gesture

    public void VolumeControlOpen(Collider collider){
        if(collider.gameObject.name == "ear"){
            debug2.text = "ear touch";
            isControllingVolume = true;
            handInitialPos = inp.posR;
            // volumeBar.SetActive(true);
        }else{debug2.text= "no ear touch";}
    }

      public void VolumeControlClose(Collider collider){
        if(collider.gameObject.name == "ear"){
            debug2.text = "ear touch";
            isControllingVolume = false;
            // volumeBar.SetActive(false);
        }else{debug2.text= "no ear touch";}
    }


    public void VolumeControlChange(Collider collider){
        // if(collider.gameObject.name == "volumebar"){
        if(isControllingVolume){
            debug2.text = (4*(inp.posR.y- handInitialPos.y)).ToString();
        }
        // }
    }


    static float TIME_FOR_QUICK_ACTION = 0.5f;

    bool MenuCheck(){

        //check hand movement over .5 seconds
        //find how many slices is .5 seconds
        // this isn't working properly
        int samplesToCheck = (int)(TIME_FOR_QUICK_ACTION/TrackingHistory.sampleRate);

        //perameters for semi straight line. Tweek it for better feeling
        float reqYdist = .3f;
        float maxXdist = .05f;

        float lowY = 0;
        float highY = 0;
        float lowX = 0;
        float highX = 0;

        float startingY=0;

        // Debug.Log("check samples" + samplesToCheck);
        for (int i = th.posListR.Count - samplesToCheck; i < th.posListR.Count; i++)
        {
            //instantiate values
            if(i == (th.posListR.Count - samplesToCheck)){
                lowY = highY = startingY = th.posListR[i].y;
                lowX = highX = th.posListR[i].x;

            }
            //log highest and lowest values
            if(th.posListR[i].y > highY){
                highY = th.posListR[i].y;
            }
            if(th.posListR[i].y < lowY){
                lowY = th.posListR[i].y;
            }
            if(th.posListR[i].y > highX){
                highX = th.posListR[i].x;
            }
            if(th.posListR[i].y > highX){
                highX = th.posListR[i].x;
            }

            if(!th.gripListR[i]){
                return false;
            }
        }
        // if within perameters and from down to up
        if((highY - lowY > reqYdist) && (highX-lowX < maxXdist) && (th.posListR[th.posListR.Count-1].y - startingY > 0)){


            // success logic
            // do the menu open thing
            cube.transform.position += new Vector3(.1f,0,0);
            return true;
        }
        return false;
    }

    // world moving stuff
    bool worldMoving;

    Vector3 worldInitialRotation;

    int samplesToCheck = (int)(TIME_FOR_QUICK_ACTION/TrackingHistory.sampleRate);

    //I'm using hand rotation to change the spin of the worlds object
    bool WorldCheck(){
        //if only grabing right controller
        if(inp.grabR && !(inp.grabR && inp.grabL)){
            //if first time starting up
            if(!worldMoving){
                handInitionRotation=inp.rotR;
                worldInitialRotation = worldsObject.transform.eulerAngles;
            }

            worldMoving = true;

            // return closest angle between the two
            // Debug.Log("dif is" + Mathf.DeltaAngle(handInitionRotation.eulerAngles.y, inp.rotR.eulerAngles.y));
            
            int lastIndex = th.rotListR.Count - samplesToCheck;
            int currentIdx = th.rotListR.Count - 1;

            if (lastIndex >= 0 && currentIdx >= 0) {
                Quaternion lastRotation = th.rotListR[lastIndex];
                Quaternion currentRotation = th.rotListR[currentIdx];

                float angleDifference = Mathf.Abs(Mathf.DeltaAngle(lastRotation.eulerAngles.y, currentRotation.eulerAngles.y));

                // Debugging information
                Debug.Log("Last Rotation: " + lastRotation.eulerAngles.y);
                Debug.Log("Current Rotation: " + currentRotation.eulerAngles.y);

                
                Debug.Log("Angle Difference: " + angleDifference);

                Debug.Log("samples to check: " + samplesToCheck + "qa" + TIME_FOR_QUICK_ACTION + "  " + TrackingHistory.sampleRate);



            } 
            else {
                // Handle cases where there aren't enough samples in rotListR
                Debug.LogWarning("Not enough samples in rotListR to perform comparison.");
            }

            //if the total angle between the first and last sample is smaller than given angle, stop spinning
            if(Mathf.Abs(Mathf.DeltaAngle(th.rotListR[th.rotListR.Count - samplesToCheck].eulerAngles.y, th.rotListR[th.rotListR.Count-1].eulerAngles.y)) < 10){
                return false;
            }

            //apply rotation force based off of how different the initial and the current rotation are.
            worldsObject.GetComponent<Rigidbody>().AddTorque(Vector3.up * (-Mathf.DeltaAngle(handInitionRotation.eulerAngles.y, inp.rotR.eulerAngles.y)*.9f));
            //remove default spin speed limit by unity
            worldsObject.GetComponent<Rigidbody>().maxAngularVelocity = float.MaxValue;

            return true;

        }else{
            //if grip gesture is not correct, turn of worldMoving
            worldMoving = false;
            return false;
        }
    }














    public WorldHolder wh;

    public Transform closestWorld= null;

    public GameObject player;

    public Material selectMaterial;

    public Material defaultMaterial;
    void SelectWorldCheck(){
        for (int i = 1; i < wh.worldList.Length; i++)
        {
            if(closestWorld == null){
                // Debug.Log("worldran");
                closestWorld = wh.worldList[i];
            }else{
                //if i world is closer than previously closest world
                if(Mathf.Abs(Vector3.Distance(wh.worldList[i].position, player.transform.position)) < Mathf.Abs(Vector3.Distance(closestWorld.position, player.transform.position))){
                    closestWorld = wh.worldList[i];
                }else{
                    wh.worldList[i].gameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
                }
            }

        }

        closestWorld.gameObject.GetComponent<MeshRenderer>().material = selectMaterial;
    }

    
    void Rescale(){

        if(!isRescaling){
            initialDistenceBetweenHands = distanceBetweenHands;
            worldInitialSize = closestWorld.localScale;
            
            isRescaling = true;
        }
        if(closestWorld!=null){

            closestWorld.localScale = worldInitialSize * (1+ Vector3.Distance(initialDistenceBetweenHands, distanceBetweenHands));
            Debug.Log(Vector3.Distance(initialDistenceBetweenHands, distanceBetweenHands));
        }

    }

    void WorldResize(){
        //cube stuff------------
        
        // direction = directionTransform.position - originTransform.position;

        distanceBetweenHands = 2*(inp.posL - inp.posR);

        // debug.text = distanceBetweenHands.ToString();


        // if(!inp.grabL){
        //     isRescaling= false;
        // }
        if(inp.grabR && inp.grabL){
            Rescale();
        }else{
            isRescaling = false;
        }

        // if(!inp.grabL && inp.grabR){
        //     if(!isGrabbing){
        //         cubepos= cube.transform.position;
        //         handInitialPos = inp.posR;
        //         isGrabbing=true;
        //     }
            
        //     cube.transform.position = cubepos + 3*(inp.posR - handInitialPos);
        // }else{
        //     isGrabbing = false;
        // }
    }

    [SerializeField] private LayerMask layerMask;

    public GameObject mainCamera;

    public GameObject rightController;

    public GameObject rayCastTarget;
    void Update()
    {
        // debug2.text = th.ListToString(th.positionListR);


        if(Time.time > 2){
            MenuCheck();
            WorldCheck();
            SelectWorldCheck();
                
                        // worldsObject.transform.Rotate(0, 5 * Time.deltaTime, 0);

        }
        


        // positionListR.Add(new Vector3(0,0,Time.time));
        // if(positionListR.Count >10){
        //     positionListR.RemoveAt(0);
        // }

        // string mystring = "";

        // for (int i = 0; i < positionListR.Count; i++)
        // {
        //     mystring = mystring + positionListR[i].ToString();
            
        // }
        // debug2.text = mystring;
        //menu gesture



        // if(inp.gripL){
        //     Debug.Log("gripingL");
        // }else if(inp.gripR){
        //     Debug.Log("gripingR");
        // }
            // if(Physics.Raycast(mainCamera.transform.position, rightController.transform.position - mainCamera.transform.position, out RaycastHit hitInfo,30f)){

            // Debug.Log("hit" + hitInfo.transform.gameObject.name);
            // try{
            //                 hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material = selectMaterial;
            // }catch{}


            // };

            if(Physics.Raycast(mainCamera.transform.position, rayCastTarget.transform.position-mainCamera.transform.position, out RaycastHit hitInfo,50f)){

            // Debug.Log("hit" + hitInfo.transform.gameObject.name);
            try{
                            hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material = selectMaterial;
            }catch{}


            };

            Debug.DrawRay(mainCamera.transform.position, rayCastTarget.transform.position-mainCamera.transform.position, Color.blue,50f);



            // if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hitInfo,50f)){

            // Debug.Log("hit" + hitInfo.transform.gameObject.name);
            // try{
            //                 hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material = selectMaterial;
            // }catch{}


            // };

            // Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward, Color.blue,50f);

         
        //  if(Physics.Raycast(inp.posH, inp.posR - inp.posH, out RaycastHit hitInfo, 20f)){
        //  };

        

        // Debug.Log("head pos" + mainCamera.transform.position + "cube" + cube.transform.position);
        // Debug.DrawRay(mainCamera.transform.position, rightController.transform.position - mainCamera.transform.position, Color.blue,20f);

         
         
        // Vector3 direction = inp.posR - inp.posH;




        // Cast a ray from posH in the direction of posR
        // Ray ray = new Ray(inp.posH, inp.posR);
        // RaycastHit hitInfo;
        // Debug.DrawLine(inp.posH, inp.posR);


        // // Check if the ray hits something
        // if (Physics.Raycast(ray, out hitInfo))
        // {
        //     Debug.Log(hitInfo);
        //     // Check if the hit point is beyond posR
        //     // if (Vector3.Dot(hitInfo.point - inp.posR, direction) > 0)
        //     // {
        //         // Debug.Log("rayc");
        //         // Get the GameObject that was hit

        //         GameObject hitObject = hitInfo.collider.gameObject;

        //         // Do something with the hitObject (e.g., print its name)
        //         Debug.Log("Hit object: " + hitObject.transform.name);
        //     // }
        // }




         // Calculate direction from point A to point B
        // Vector3 direction = pointB.position - pointA.position;

        // // Cast a ray from point A in the direction of point B
        // Ray ray = new Ray(pointA.position, direction);
        // RaycastHit hitInfo;

        // // Check if the ray hits something
        // if (Physics.Raycast(ray, out hitInfo))
        // {
        //     // Check if the hit point is beyond point B
        //     if (Vector3.Dot(hitInfo.point - pointB.position, direction) > 0)
        //     {
        //         // Get the GameObject that was hit
        //         GameObject hitObject = hitInfo.collider.gameObject;

        //         // Do something with the hitObject (e.g., print its name)
        //         Debug.Log("Hit object: " + hitObject.name);
        //     }
        // }

































































        //cube stuff------------
        
        direction = directionTransform.position - originTransform.position;

        distanceBetweenHands = 2*(inp.posL - inp.posR);

        // debug.text = distanceBetweenHands.ToString();


        if(!inp.grabL){
            isRescaling= false;
        }
        if(inp.grabR && inp.grabL){
            Rescale();
        }

        if(!inp.grabL && inp.grabR){
            if(!isGrabbing){
                cubepos= cube.transform.position;
                handInitialPos = inp.posR;
                isGrabbing=true;
            }
            
            cube.transform.position = cubepos + 3*(inp.posR - handInitialPos);
        }else{
            isGrabbing = false;
        }




        // if((inp.posL - inp.posH).magnitude < 5){
        //     debug.text = "close";
        // }

        // debug.text= inp.stickL.ToString();




        float rotationAmount = inp.stickR.x * 1 * Time.deltaTime;

        // Rotate the player around the Y-axis
        // player.transform.Rotate(0, rotationAmount, 0);

        // player.rotate += inp.stickR.x;


        // debug2.text= (inp.posL - inp.posH).magnitude.ToString();

        // if (Physics.Raycast(originTransform.position, direction, out RaycastHit hit))
        // {
        //     Ray hit something, do something with the hit information
        //     debug.text= hit.collider.name;
        // }



        // Debug.DrawRay(originTransform.position, direction * 10, Color.red);





    }
}

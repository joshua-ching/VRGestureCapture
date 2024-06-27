using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net.NetworkInformation;
using JetBrains.Annotations;
using UnityEngine.Experimental.AI;

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

    Vector3 cubeinitialsize;

    Vector3 handinitialpos;

    public GameObject worldsObject;


    // public GameObject leftHand;

    // public GameObject volumeBar;

    // public GameObject focusHand;

    bool isControllingVolume;





    void Rescale(){

        if(!isRescaling){
            initialDistenceBetweenHands = distanceBetweenHands;
            cubeinitialsize = cube.transform.localScale;
            
            isRescaling = true;
        }
            cube.transform.localScale = cubeinitialsize + 2*(initialDistenceBetweenHands - distanceBetweenHands);

    }

    //volume gesture

    public void VolumeControlOpen(Collider collider){
        if(collider.gameObject.name == "ear"){
            debug2.text = "ear touch";
            isControllingVolume = true;
            handinitialpos = inp.posR;
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
            debug2.text = (4*(inp.posR.y- handinitialpos.y)).ToString();
        }
        // }
    }



    bool MenuCheck(){

        //check hand movement over .5 seconds
        //find how many slices is .5 seconds
        // this isn't working properly
        int samplesToCheck = (int)(0.5/th.sampleRate);

        //perameters for semi straight line. Tweek it for better feeling
        float reqYdist = .3f;
        float maxXdist = .05f;

        float lowY = 0;
        float highY = 0;
        float lowX = 0;
        float highX = 0;

        float startingY=0;

        // Debug.Log("check samples" + samplesToCheck);
        for (int i = th.positionListR.Count - samplesToCheck; i < th.positionListR.Count; i++)
        {
            //instantiate values
            if(i == (th.positionListR.Count - samplesToCheck)){
                lowY = highY = startingY = th.positionListR[i].y;
                lowX = highX = th.positionListR[i].x;

            }
            //log highest and lowest values
            if(th.positionListR[i].y > highY){
                highY = th.positionListR[i].y;
            }
            if(th.positionListR[i].y < lowY){
                lowY = th.positionListR[i].y;
            }
            if(th.positionListR[i].y > highX){
                highX = th.positionListR[i].x;
            }
            if(th.positionListR[i].y > highX){
                highX = th.positionListR[i].x;
            }

            if(!th.gripListR[i]){
                return false;
            }
        }
        // if within perameters and from down to up
        if((highY - lowY > reqYdist) && (highX-lowX < maxXdist) && (th.positionListR[th.positionListR.Count-1].y - startingY > 0)){


            // success logic
            Debug.Log("succcccc" + Time.time);
                // do the menu open thing
            cube.transform.position += new Vector3(.1f,0,0);
            return true;
        }
        return false;
    }

    
    bool worldMoving;
    void WorldCheck(){
        if(inp.grabR){

            if(!worldMoving){
                handinitialpos = inp.posR;
            }
            worldMoving = true;

            // worldsObject

            if(worldMoving){
                worldsObject.transform.Rotate(0,  3*(handinitialpos.x -  inp.posR.x), 0);
            }


            // worldsObject.transform.Rotate(0, 5 * Time.deltaTime, 0);
            //rotate world based off of hand movement from side to side from when start gripping
            // worldsObject.transform.Rotate(0, 5, 0);
        }else{
            worldMoving = false;
        }
    }

    
    void Update()
    {
        // debug2.text = th.ListToString(th.positionListR);


        if(Time.time > 2){
            MenuCheck();
            WorldCheck();
                
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



        if(inp.gripL){
            Debug.Log("gripingL");
        }else if(inp.gripR){
            Debug.Log("gripingR");
        }


































































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
                handinitialpos = inp.posR;
                isGrabbing=true;
            }
            
            cube.transform.position = cubepos + 3*(inp.posR - handinitialpos);
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

using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;
using UnityEngine.Animations;

//attached to hmd component
public class RaycastTargetCalibrator : MonoBehaviour
{

    public GameObject target;

    public GameObject inBetweenObject;//sphere

    public GameObject onTopObject;

    public GameObject handObject;

    public InputData inp;

    public PositionConstraint pc;

    float distance;
    // 0 is lazer, 1 is head hand, and 2 is head hand offset
    int selectionMode;

    void Update()
    {
        if (inp.triggerL && !firing)
        {
            Fire();
        }
        MoveTargetSphereOffeset(inp.rotR.y, 'z'); 
        MoveTargetSphereOffeset(inp.rotR.x, 'y'); 

        Debug.Log(inp.rotR.z);
        Debug.Log("we");

        // MoveTargetSphere(inp.rotR.eulerAngles);

    }

    bool firing = false;

    //this no longer needs to be a coroutine?
    public void Fire()
    {
        firing = true;
        //==================fire ray to find where sphere should go================

        if (Physics.Raycast(transform.position, target.transform.position - transform.position, out RaycastHit hit))
        {
            // Log the name of the object hit
            // Debug.Log("Hit: firing" + hit.collider.name);
            // Draw the ray in the Scene view
            // Debug.DrawRay(transform.position, targetList[target].transform.position - transform.position, Color.red, 100f);               
        }

        // yield return new WaitForSeconds(0.00001f);

        //=================sphere positioning stuff =============================

        //setting distence of object from head to be based off of distance from hand to head?
        distance = Vector3.Distance(handObject.transform.position,transform.position);
        //unlock position constraint so values can be manipulated
        pc.locked = false;
        pc.constraintActive = false;
        //get direction of hand to sphere. extend to length of distence
        inBetweenObject.transform.position = ((target.transform.position - transform.position).normalized * distance) + transform.position;

        pc.translationAtRest = inBetweenObject.transform.position;
        //FIXED USE HAND TRANSFORM NOT HEAD TRANSFORM
        pc.translationOffset = inBetweenObject.transform.position - handObject.transform.position;

        // yield return new WaitForSeconds(1);
        pc.constraintActive = true;
        pc.locked = true;

        //=========================================================================
        originalRotation = inp.rotR;
        
        firing = false;
    }

    Vector3 inBetweenObjectOriginalPosition;


    public void MoveTargetSphereAbsolute(Vector3 offset){

        //unlock position constraint so values can be manipulated
        pc.locked = false;
        pc.constraintActive = false;


        // inBetweenObjectOriginalPosition = ;

        inBetweenObject.transform.position = inBetweenObject.transform.position + offset;

        //set new position
        //get direction of hand to sphere. extend to length of distence
        // inBetweenObject.transform.position = ((target.transform.position - transform.position).normalized * distance) + transform.position;

        pc.translationAtRest = inBetweenObject.transform.position;
        //FIXED USE HAND TRANSFORM NOT HEAD TRANSFORM
        pc.translationOffset = inBetweenObject.transform.position - handObject.transform.position;

        // yield return new WaitForSeconds(1);
        pc.constraintActive = true;
        pc.locked = true;


    }

    Vector3 originalPosition;

    Quaternion originalRotation;

    int movementScale = 20;

    public void MoveTargetSphereOffeset(float offset, char axis){
        originalPosition = onTopObject.transform.localPosition;


        //invert because for some reason its opposite
            offset=offset*-1;
            

        

            // if(originalRotation.y > 0){
            // offset = offset - originalRotation.y;
            // }else{
            //     offset = offset + originalRotation.y;
            // }




        //oto z axis times movement scale to amplify movement speed/sensitivity
        if(axis == 'z'){
            // offset=offset*-1;

            // if(originalRotation.y > 0){
            // offset = offset - originalRotation.y;
            // }else{
                offset = offset + originalRotation.y;
            // }

            onTopObject.transform.localPosition = new Vector3(onTopObject.transform.localPosition.x,onTopObject.transform.localPosition.y,movementScale*offset);
        }else if(axis == 'y'){

            // if(originalRotation.x > 0){
            // offset = offset - originalRotation.x;
            // }else{
                offset = offset + originalRotation.x;
            // }
            
            onTopObject.transform.localPosition = new Vector3(onTopObject.transform.localPosition.x,movementScale*offset,onTopObject.transform.localPosition.z);
        }

    }
}

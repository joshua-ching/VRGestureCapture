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
        
        firing = false;
    }

    Vector3 inBetweenObjectOriginalPosition;


    public void MoveTargetSphere(Vector3 offset){

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
}

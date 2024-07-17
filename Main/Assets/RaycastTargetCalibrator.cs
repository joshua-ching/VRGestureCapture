using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RaycastTargetCalibrator : MonoBehaviour
{

    public GameObject target;

    public GameObject inBetweenObject;

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
            
            StartCoroutine(Fire());
        }

    }

    bool firing = false;
    public IEnumerator Fire()
    {
        firing = true;

        if (Physics.Raycast(transform.position, target.transform.position - transform.position, out RaycastHit hit))
        {
            // Log the name of the object hit
            // Debug.Log("Hit: firing" + hit.collider.name);
            // Draw the ray in the Scene view
            // Debug.DrawRay(transform.position, targetList[target].transform.position - transform.position, Color.red, 100f);               
        }

        yield return new WaitForSeconds(1f);
        //setting distence of object from head to be based off of distance from hand to head?
        distance = Vector3.Distance(handObject.transform.position,transform.position);

        pc.locked = false;

        pc.constraintActive = false;
        inBetweenObject.transform.position = ((target.transform.position - transform.position).normalized * distance) + transform.position;

        distance = Vector3.Distance(handObject.transform.position, transform.position);

        pc.translationAtRest = inBetweenObject.transform.position;
        //FIXED USE HAND TRANSFORM NOT HEAD TRANSFORM
        pc.translationOffset = (inBetweenObject.transform.position - handObject.transform.position);

        // yield return new WaitForSeconds(1);
        pc.constraintActive = true;
        pc.locked = true;
        
        firing = false;
    }
}

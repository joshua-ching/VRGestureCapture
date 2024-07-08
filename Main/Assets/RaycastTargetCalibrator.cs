using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RaycastTargetCalibrator : MonoBehaviour
{

    public List<GameObject> targetList = new List<GameObject>();
    public GameObject inBetweenObject;

    public GameObject handObject;

    public InputData inp;

    public PositionConstraint pc;

    int target;

    float distance;

    // Start is called before the first frame update
    void Start()
    {
                    // StartCoroutine(Fire());

    }

    // Update is called once per frame
    void Update()
    {
        if(inp.triggerL && !firing){
            StartCoroutine(Fire());
        }
       
    }

    bool firing = false;
    IEnumerator Fire(){
        firing = true;

        if(target == targetList.Count){
            target = 0;
        }
        if (Physics.Raycast(transform.position, targetList[target].transform.position - transform.position, out RaycastHit hit))
            {
                // Log the name of the object hit
                Debug.Log("Hit: firing" + hit.collider.name);
                // Draw the ray in the Scene view
                Debug.DrawRay(transform.position, targetList[target].transform.position - transform.position, Color.red, 100f);               
            }

        yield return new WaitForSeconds(1f);
        distance = Vector3.Distance(handObject.transform.position,transform.position);

        // pc.enabled = false;
        // pc.locked = false;

        // pc.constraintActive = false;
        inBetweenObject.transform.position = ((targetList[target].transform.position - transform.position).normalized *distance) + transform.position;

        // inBetweenObject.transform.position =new Vector3(1,1,1);
        // pc.translationAtRest = inBetweenObject.transform.position;
        // pc.translationOffset = 
        // pc.translationAtRest = inBetweenObject.transform.position;sct.transform.position;
        yield return new WaitForSeconds(1);

        // pc.
        // pc.constraintActive = true;

        // pc.locked = true;

        target++;


        firing = false;
    }
}

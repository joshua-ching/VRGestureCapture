using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class testRaycaster : MonoBehaviour
{

    public List<GameObject> targetList = new List<GameObject>();
    // Start is called before the first frame update

    public GameObject inBetweenObject;

    public GameObject handObject;

    PositionConstraint pc;

    int target = 0;

    float distence;

    void Start()
    {
        pc = inBetweenObject.GetComponent<PositionConstraint>();
         StartCoroutine(Shoot());

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator Shoot()
    {

        if(target == targetList.Count){
            target = 0;
        }
        if (Physics.Raycast(transform.position, targetList[target].transform.position - transform.position, out RaycastHit hit))
            {
                // Log the name of the object hit
                Debug.Log("Hit: " + hit.collider.name);

                // Draw the ray in the Scene view
                Debug.DrawRay(transform.position, targetList[target].transform.position - transform.position, Color.red, 100f);               
            }



        distence = Vector3.Distance(handObject.transform.position,transform.position);
        

        pc.locked = false;
                yield return new WaitForSeconds(1f);

        pc.constraintActive = false;
        inBetweenObject.transform.position = ((targetList[target].transform.position - transform.position).normalized *distence) + transform.position;
        yield return new WaitForSeconds(1f);
        pc.translationAtRest = inBetweenObject.transform.position;
        pc.translationOffset = inBetweenObject.transform.position - handObject.transform.position;

        pc.constraintActive = true;
        // pc.locked = true;



        target++;

        yield return new WaitForSeconds(1);

        StartCoroutine(Shoot());
    
    }
}

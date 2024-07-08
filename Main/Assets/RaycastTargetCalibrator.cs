using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;

public class RaycastTargetCalibrator : MonoBehaviour
{

    public List<GameObject> targetList = new List<GameObject>();
    public GameObject inBetweenObject;

    public GameObject handObject;

    public InputData inp;

    public PositionConstraint pc;

    public GameObject raycastPrefab;

    GameObject spawnedRaycast;

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
        //so doesn't fire again until done
        firing = true;
        //reset index if out of index
        if(target == targetList.Count){
            target = 0;
        }
        //cast ray from headset to target
        if (Physics.Raycast(transform.position, targetList[target].transform.position - transform.position, out RaycastHit hit))
            {
                // Log the name of the object hit
                Debug.Log("Hit: firing" + hit.collider.name);
                // Draw the ray in the Scene view
                Debug.DrawRay(transform.position, targetList[target].transform.position - transform.position, Color.red, 100f);               
            }

        yield return new WaitForSeconds(1f);
        distance = Vector3.Distance(handObject.transform.position,transform.position);

        var targetLocation = ((targetList[target].transform.position - transform.position).normalized *distance) + transform.position;

        inBetweenObject.transform.position = targetLocation;
        spawnedRaycast = Instantiate(raycastPrefab, targetLocation + new Vector3(0,0.2f,0), Quaternion.identity);

        ConstraintSource c = new ConstraintSource();
        c.sourceTransform = handObject.transform;
        c.weight = .1f;
        spawnedRaycast.GetComponent<PositionConstraint>().AddSource(c);

        spawnedRaycast.GetComponent<PositionConstraint>().constraintActive = true;
        spawnedRaycast.GetComponent<PositionConstraint>().locked = true;


        target++;
        firing = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class RayCastTargetBehavior : MonoBehaviour
{

     public InputData inp;

    public GameObject hand;

    public GameObject callibrator;

    PositionConstraint pc;

    Vector3 offset;

    float distence;
    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PositionConstraint>();
        // offset = GetComponent<Transform>().position - hand.transform.position;
    }


    bool recallibrating = false;
    IEnumerator Recallibrate(){
        recallibrating = true;
        pc.locked = false;
                yield return new WaitForSeconds(1f);

                        distence = Vector3.Distance(hand.transform.position,transform.position);


            pc.constraintActive = false;
            transform.position = ((callibrator.transform.position - transform.position).normalized *distence) + transform.position;
            yield return new WaitForSeconds(1f);
            pc.translationAtRest = transform.position;
            pc.translationOffset = transform.position - hand.transform.position;

            pc.constraintActive = true;
            Debug.Log("ltrigger");
    }
    // Update is called once per frame
    void Update()
    {

        // GetComponent<Transform>().position = new Vector3(0f, Time.time, 0);

        GetComponent<Transform>().position = hand.transform.position +  new Vector3(.5f,0,.5f);

        //local position?




                // GetComponent<Transform>().position = hand.transform.position + (hand.transform.position - new Vector3(-.05f,.05f,0));


        // GetComponent<Transform>().position =  hand.transform.position +  hand.transform.rotation * offset;
        // Debug.Log("control pos " + hand.transform.position + "point pos " + GetComponent<Transform>().position);

        //fun
        // GetComponent<Transform>().RotateAround(hand.transform.position, Vector3.right, inp.rotR.z * 20f);

        if(inp.triggerL){
            if(!recallibrating){

            // StartCoroutine(Recallibrate());
            }
        }
    }



}

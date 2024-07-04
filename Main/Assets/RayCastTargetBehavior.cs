using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastTargetBehavior : MonoBehaviour
{

     public InputData inp;

    public GameObject hand;

    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        // offset = GetComponent<Transform>().position - hand.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        // GetComponent<Transform>().position = new Vector3(0f, Time.time, 0);

        GetComponent<Transform>().position = hand.transform.position +  new Vector3(.5f,0,.5f);

        //local position?




                // GetComponent<Transform>().position = hand.transform.position + (hand.transform.position - new Vector3(-.05f,.05f,0));


        // GetComponent<Transform>().position =  hand.transform.position +  hand.transform.rotation * offset;
        Debug.Log("control pos " + hand.transform.position + "point pos " + GetComponent<Transform>().position);

        //fun
        // GetComponent<Transform>().RotateAround(hand.transform.position, Vector3.right, inp.rotR.z * 20f);
    }
}

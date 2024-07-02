using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastTargetBehavior : MonoBehaviour
{

     public InputData inp;

    public GameObject hand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<Transform>().position = new Vector3(.1f, .1f, inp.rotR.z);


        //fun
        // GetComponent<Transform>().RotateAround(hand.transform.position, Vector3.up, inp.rotR.z * 20f);
    }
}

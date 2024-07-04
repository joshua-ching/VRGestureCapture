using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRotation : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform t;

    void Awake(){

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

                                t.rotation = new Quaternion(0,0,0,0);
                                // Debug.Log(t.rotation);


                    // We remove the Y (up) part of the forward vectors to keep things flat
    // e.g. like a top-down 2d projection
    // Vector3 playerForwardFlat = new Vector3( camForward.x, 0, camForward.z ).normalized;
    // Vector3 desiredForwardFlat = new Vector3( spawnPointForward.x, 0, spawnPointForward.z ).normalized;
 
    // // since we're comparing against an up vec, the param order here is important
    // float angle = Vector3.SignedAngle( playerForwardFlat, desiredForwardFlat, Vector3.up );
 
    // // if facing right of the target, angle will be neg
    // // if facing left, angle will be pos
    // Vector3 targetAngle = new Vector3( 0, angle, 0 );
 
    // // Rotate
    // xrOrigin.transform.position = spawnPoint.position;
    // xrOrigin.transform.eulerAngles = targetAngle;

    }
}

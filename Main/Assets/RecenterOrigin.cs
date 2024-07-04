using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;

public class RecenterOrigin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Recenter();
    }



    public Transform head;

    public Transform origin;
    public Transform target;

    bool hasRecentered = false;
    public void Recenter(){

        Vector3 offset = head.position - origin.position;
        offset.y = 0;
        origin.position = target.position - offset;

        Vector3 targetForward=target.forward;
        targetForward.y = 0;
        Vector3 cameraforward=head.forward;
        cameraforward.y=0;

        float angle = Vector3.SignedAngle(cameraforward, targetForward, Vector3.up);

        origin.RotateAround(head.position, Vector3.up, angle);
        // XROrigin xrOrigin = GetComponent<XROrigin>();
        // xrOrigin.MoveCameraToWorldLocation(new Vector3(0,0,0));
        // xrOrigin.MatchOriginUpCameraForward(target.up, target.forward);


    }

    // Update is called once per frame
    void Update()
    {

        // if(head.rotation == target.rotation){
        //     Debug.Log("true");
        //     hasRecentered=true;
        // }
        
        if(Time.time<0.1f){
            Recenter();
        }
                                    


    }
}

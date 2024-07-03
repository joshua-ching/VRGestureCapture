using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManipulation : MonoBehaviour
{

    public InputData inp;

    public GestureManagement gest;

    public TrackingHistory th;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    bool rotating;

    Vector3 objectInitialRotation;

    int samplesToCheck = (int)(GestureManagement.TIME_FOR_QUICK_ACTION / TrackingHistory.sampleRate);

    Quaternion handInitionRotation;


    public bool RotateObject(GameObject objectToRotate)
    {

        //I'm using hand rotation to change the spin of the object

        //if only grabing right controller
        if (inp.grabR && !(inp.grabR && inp.grabL))
        {
            //if first time starting up
            if (!rotating)
            {
                handInitionRotation = inp.rotR;
                objectInitialRotation = objectToRotate.transform.eulerAngles;
            }

            rotating = true;
            //calculate which from which to which index should be checked based off of samplesToCheck
            int lastIndex = th.rotListR.Count - samplesToCheck;
            int currentIdx = th.rotListR.Count - 1;

            if (lastIndex >= 0 && currentIdx >= 0)
            {
                //get rotation angles from most recent and last rotation angle
                Quaternion lastRotation = th.rotListR[lastIndex];
                Quaternion currentRotation = th.rotListR[currentIdx];

                float angleDifference = Mathf.Abs(Mathf.DeltaAngle(lastRotation.eulerAngles.y, currentRotation.eulerAngles.y));
            }
            else
            {
                // Handle cases where there aren't enough samples in rotListR
                Debug.LogWarning("Not enough samples in rotListR to perform comparison.");
            }
            //if the total angle between the first and last sample is smaller than given angle, stop spinning
            if (Mathf.Abs(Mathf.DeltaAngle(th.rotListR[th.rotListR.Count - samplesToCheck].eulerAngles.y, th.rotListR[th.rotListR.Count - 1].eulerAngles.y)) < 10)
            {
                return false;
            }

            //apply rotation force based off of how different the initial and the current rotation are.
            objectToRotate.GetComponent<Rigidbody>().AddTorque(Vector3.up * (-Mathf.DeltaAngle(handInitionRotation.eulerAngles.y, inp.rotR.eulerAngles.y) * .9f));
            //remove default spin speed limit by unity
            objectToRotate.GetComponent<Rigidbody>().maxAngularVelocity = float.MaxValue;

            return true;

        }
        else
        {
            //if grip gesture is not correct, turn of worldMoving
            rotating = false;
            return false;
        }
    }

    bool isRescaling;

    Vector3 initialDistenceBetweenHands;

    Vector3 objectInitialSize;

    
    public bool Rescale(GameObject objectToRescale){

        if(!isRescaling){
            initialDistenceBetweenHands = gest.distanceBetweenHands;
            objectInitialSize = objectToRescale.transform.localScale;
            
            isRescaling = true;
            
        }
        if(objectToRescale!=null){

            objectToRescale.transform.localScale = objectInitialSize * (1+ Vector3.Distance(initialDistenceBetweenHands, gest.distanceBetweenHands));
            Debug.Log(Vector3.Distance(initialDistenceBetweenHands, gest.distanceBetweenHands));
            return true;
        }
        return false;

    }
}


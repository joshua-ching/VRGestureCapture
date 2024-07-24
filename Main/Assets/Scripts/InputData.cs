using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.XR;

public class InputData : MonoBehaviour
{

    // public delegate void TriggerActionL();
    // public static event TriggerActionL TriggerClick;
    public InputDevice _rightController;
    public InputDevice _leftController;
    public InputDevice _HMD;



    //Head Variables===============================
    public Vector3 posH;

    public Quaternion rotH;

    public GameObject mainCamera;



    //Left Hand Variables==========================
 
    public Vector3 posL;
    public bool triggerL;

    public bool gripL;

    public bool grabL;

    public bool AButtonL;
    public bool BButtonL;

    

    public Vector2 stickL;



    

    //Right Hand Variables=========================
    public Vector3 posR;

    public bool triggerR;

    public bool gripR;

    public bool grabR;

    public Vector2 stickR;

    public Quaternion rotR;


    public TextMeshProUGUI log;

    public bool isDoublePointing;




    void Update()
    {
        // log.text=stickL.ToString();



        if (!_rightController.isValid || !_leftController.isValid || !_HMD.isValid){
            InitializeInputDevices();
        }

        //Head Values==============================================



        if (_HMD.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 headPosition))
        {
            posH = headPosition;
        }

        rotH = mainCamera.transform.rotation;



        //Left Hand Values==============================================s



        //(Could get rotation and position data from physical game object?)

        if (_leftController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 handPositionLeft))
        {
            posL = handPositionLeft;
        }

        if (_leftController.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerStateLeft))
        {
            triggerL= triggerStateLeft;
        }
        

        if (_leftController.TryGetFeatureValue(CommonUsages.gripButton, out bool gripStateLeft))
        {
            gripL=gripStateLeft;
        }

        if (_leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 stickPositionLeft))
        {
            stickL = stickPositionLeft;
            // stickL= Vector2.zero;
        }

        if (_leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool AbuttonLeft))
        {
            AButtonL = AbuttonLeft;
        }

        if (_leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool BbuttonLeft))
        {
            BButtonL = BbuttonLeft;
        }

        
        //right hand


        if (_rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 handPositionRight))
        {
            posR= handPositionRight;
        }


        if (_rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerStateRight))
        {
            triggerR= triggerStateRight;
        }

        if (_rightController.TryGetFeatureValue(CommonUsages.gripButton, out bool gripStateRight))
        {
            gripR= gripStateRight;
        }

        if (_rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 stickPositionRight))
        {
            stickR = stickPositionRight;
        }

        if (_rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotationRight))
        {
            rotR = rotationRight;
        }


        

        //=========================================
        if(triggerL && gripL){
            grabL = true;
        }else{
            grabL = false;
        }

        if(triggerR && gripR){
            grabR = true;
        }else{
            grabR = false;
        }



        if(gripR && gripL){
            isDoublePointing=true;


        }else{isDoublePointing=false;}



        // //events

        // if(triggerL){
            
        // }

    }





    private void InitializeInputDevices()
    {
        
        if(!_rightController.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, ref _rightController);
        if (!_leftController.isValid) 
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, ref _leftController);
        if (!_HMD.isValid) 
            InitializeInputDevice(InputDeviceCharacteristics.HeadMounted, ref _HMD);

    }

    private void InitializeInputDevice(InputDeviceCharacteristics inputCharacteristics, ref InputDevice inputDevice)
    {
        List<InputDevice> devices = new List<InputDevice>();
        //Call InputDevices to see if it can find any devices with the characteristics we're looking for
        InputDevices.GetDevicesWithCharacteristics(inputCharacteristics, devices);

        //Our hands might not be active and so they will not be generated from the search.
        //We check if any devices are found here to avoid errors.
        if (devices.Count > 0)
        {
            inputDevice = devices[0];
        }
    }




    

}

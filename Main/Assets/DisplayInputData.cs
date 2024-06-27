using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using System;
using Unity.VisualScripting;

[RequireComponent(typeof(InputData))]
public class DisplayInputData : MonoBehaviour
{
    public TextMeshProUGUI positionLeftText;
    public TextMeshProUGUI rotationLeftText;
    public TextMeshProUGUI triggerLeftText;

    public TextMeshProUGUI gripLeftText;


    public TextMeshProUGUI positionRightText;

    public TextMeshProUGUI rotationRightText;

    public TextMeshProUGUI triggerRightText;

    public TextMeshProUGUI gripRightText;


    private InputData _inputData;
    private float _leftMaxScore = 0f;
    private float _rightMaxScore = 0f;



    // void DisplayData(String device, CommonUsages aspect, Variable type){

    // }
    private void Start()
    {
        _inputData = GetComponent<InputData>();
    }



// Assuming _inputData is already defined and initialized somewhere in your code.

    // Example method for retrieving controller data
    // private void RetrieveControllerData<T>(UnityEngine.XR.InputDevice controller, InputFeatureUsage<T> usage, UnityEngine.UI.Text displayText)
    // {
    //     if (controller.TryGetFeatureValue(usage, out T value))
    //     {
    //         displayText.text = value.ToString();
    //     }
    // }

    // private void Update()
    // {
    //     // Example usage for left controller rotation
    //     RetrieveControllerData(_inputData._leftController, CommonUsages.deviceRotation, rotationLeftText);

    //     // Example usage for left controller trigger button state
    //     RetrieveControllerData(_inputData._leftController, CommonUsages.triggerButton, triggerLeftText);

    //     // Add more similar calls for other controller data as needed
    // }
    
    
    
        void Update()
    {
        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotationLeft))
        {
            rotationLeftText.text= rotationLeft.ToString();
        }

        
            triggerLeftText.text= _inputData.triggerL.ToString();
        

        
            gripLeftText.text= _inputData.gripL.ToString();
        

        
            positionLeftText.text= _inputData.posL.ToString();
        





        


        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotationRight))
        {
            rotationRightText.text= rotationRight.ToString();
        }

        
            triggerRightText.text= _inputData.triggerR.ToString();
        


        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 positionRight))
        {
            positionRightText.text= positionRight.ToString();
        }

            gripRightText.text= _inputData.gripR.ToString();
        
    }
}

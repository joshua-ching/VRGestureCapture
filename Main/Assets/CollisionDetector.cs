using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ColEvent : UnityEvent<Collider>{

}
public class CollisionDetector : MonoBehaviour
{

    public ColEvent touch;

    public ColEvent exit;

    public ColEvent stay;
    public Collider col;
    void Start()
    {
        col = GetComponent<Collider>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log("collided with" + other);
        touch.Invoke(other);
    }

    private void OnTriggerExit(Collider other){
        Debug.Log("exited " + other);
        exit.Invoke(other);
    }

    private void OnTriggerStay(Collider other){
        // Debug.Log("stay " + other);
        stay.Invoke(other);
    }

    



    
}

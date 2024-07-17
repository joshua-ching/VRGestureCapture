using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldHolder : MonoBehaviour
{

    public Transform[] worldList;
    // Start is called before the first frame update
    void Start()
    {

        worldList = gameObject.GetComponentsInChildren<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

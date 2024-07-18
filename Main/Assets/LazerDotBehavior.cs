using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerDotBehavior : MonoBehaviour
{

    float timeToWait;

    float initialTime;

    float timeToDie =.1f;
    // Start is called before the first frame update
    void Start()
    {
        initialTime = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > initialTime + timeToDie){
            Destroy(transform.gameObject);
        }
    }
}

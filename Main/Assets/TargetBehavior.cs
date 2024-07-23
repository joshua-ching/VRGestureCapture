using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour
{

    // float timeTillDeath;

    float timeToWait = 5;

    float deathTime;

    float countDown;
    // Start is called before the first frame update
    void Start()
    {
        deathTime = Time.time + timeToWait;
    }

    // Update is called once per frame
    void Update()
    {
        countDown = deathTime - Time.time;
    }


    public void OnSelected(){
        Debug.Log("rannnnn");
        // Destroy(this.gameObject);
        transform.position = Vector3.zero;
    }

    //count down till death
    //count even negative time.
    //log it.
}

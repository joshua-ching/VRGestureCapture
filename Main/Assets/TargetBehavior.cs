using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour
{

    // float timeTillDeath;

    float timeToWait = 5;

    float deathTime;

    float countDown;

    public GameManager gm;

    float startTime;

    public InputData inp;

    // public InputData inp;
    // Start is called before the first frame update
    void Start()
    {
        // deathTime = Time.time + timeToWait;
        gm=GameObject.Find("Game Manager").GetComponent<GameManager>();
        inp=GameObject.Find("Input Manager").GetComponent<InputData>();

        // gm.Test2();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // countDown = deathTime - Time.time;
    }

    bool hasBeenSelected = false;
    public void OnSelected(){
        if(!hasBeenSelected){
            // Destroy(this.gameObject);

            // .hits++;
            gm.hits++;
            hasBeenSelected = true;

            gm.WriteTimeToCSV((Time.time-startTime) + "," + transform.localScale.x );//time elapsed since spawn. how fast it was clicked
            //fix rotatiuon thing

            Debug.Log("rannnnn");            
            transform.position = Vector3.zero;

        }
        
    }

    //count down till death
    //count even negative time.
    //log it.
}

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

    public float startTime;

    public InputData inp;

    int initialMisses;

    // public InputData inp;
    // Start is called before the first frame update
    void Start()
    {
        // deathTime = Time.time + timeToWait;
        gm=GameObject.Find("Game Manager").GetComponent<GameManager>();
        inp=GameObject.Find("Input Manager").GetComponent<InputData>();
        initialMisses = gm.misses;

        // gm.Test2();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // countDown = deathTime - Time.time;
    }

    public bool hasBeenSelected = false;
    public void OnSelected(){
        if(!hasBeenSelected){
            // Destroy(this.gameObject);

            // .hits++;
            gm.hits++;
            gm.WriteToCSV((Time.time-startTime) + "," + transform.localScale.x + "," + (gm.misses - initialMisses));//time elapsed since spawn. how fast it was clicked
            hasBeenSelected = true;

            //fix rotatiuon thing

            Debug.Log("rannnnn");            
            transform.position = Vector3.zero;

        }
        
    }

    //count down till death
    //count even negative time.
    //log it.
}
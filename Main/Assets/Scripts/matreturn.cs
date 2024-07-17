using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class matreturn : MonoBehaviour
{
    // Start is called before the first frame update

    MeshRenderer mr;

     Material defaultmat;
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        defaultmat = mr.material;
    }

    float waitTime = 0;

    float timeToWait = 10;

    // Update is called once per frame
    void Update()
    {
        if(mr.material != defaultmat){
            // waitTime= Time.time + timeToWait;
            if(Time.time > waitTime){
                mr.material = defaultmat;
                waitTime += timeToWait;
            }
        }
    }
}

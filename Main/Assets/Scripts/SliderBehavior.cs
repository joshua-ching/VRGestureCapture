using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SliderBehavior : MonoBehaviour
{

    public AudioSource bg;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.SetLocalPositionAndRotation(new Vector3(transform.localPosition.x, (bg.volume * 100) - 50, transform.localPosition.z), quaternion.identity);
    }
}

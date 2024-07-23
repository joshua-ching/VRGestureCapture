using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleParentBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
public Transform target;
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Calculate the direction from the pole to the target
            Vector3 direction = target.position - transform.position;

            // Rotate the pole to point towards the target
            transform.rotation = Quaternion.LookRotation(direction);
                transform.Rotate(90, 0, 0, Space.Self);

        }
    }
}

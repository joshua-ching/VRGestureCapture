using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleBehavior : MonoBehaviour
{

    public InputData inp;

    public MeshRenderer childMesh;

        public MeshRenderer parentMesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   
        public Transform target;

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

        if(inp.gripR){
            //childMesh.enabled = true;
                        parentMesh.enabled = true;

        }else{
                        childMesh.enabled = false;
                                    parentMesh.enabled = false;


        }

        //  if (target != null)
        // {
        //     // Calculate the direction from the pole to the target
        //     Vector3 direction = target.position - transform.position;

        //     // Find a perpendicular direction
        //     Vector3 perpendicularDirection = Vector3.Cross(direction, Vector3.up);

        //     // Rotate the pole to point perpendicularly to the target
        //     transform.rotation = Quaternion.LookRotation(perpendicularDirection);
        // }
    }
    
}

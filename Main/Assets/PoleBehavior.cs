using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class PoleBehavior : MonoBehaviour
{

    public InputData inp;

    public MeshRenderer childMesh;

    public MeshRenderer parentMesh;

    public CapsuleCollider cc;

    public GestureManagement gm;

    public List<GameObject> selectedObjects = new List<GameObject>();

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   
        
    GameObject closest;
        public MeshRenderer cursormMesh;

    void Update()
    {
        

        if(inp.gripR){
            childMesh.enabled = true;
            parentMesh.enabled = true;
            cursormMesh.enabled=true;


        }else{
                        childMesh.enabled = false;
                        parentMesh.enabled = false;
                                    cursormMesh.enabled=false;


        }

        if(inp.triggerR){

            if(gameManager.selectionType == 0){



                if(gm.GetConfirmRayObject() == gm.selectedObject){//make sure ray is still pointing at object cause no leave function like pole

                gm.Select(gm.selectedObject);
                };
            }
            else if(gameManager.selectionType == 1){

            
                for(int i=0;i< selectedObjects.Count; i++){
                    // try{

                    gm.Select(selectedObjects[i]);
                    // }catch{
                    //     Debug.Log("XX tried to select " + selectedObjects[i].name);

                    // }
                }
            }
            Debug.Log("fired");

            // for(int i=0;i< selectedObjects.Count; i++){
            //     if(i==0){
            //         closest = selectedObjects[0];
            //     }else{
            //         //if current object is closer to center of pole than the current closest object
            //         if(Vector3.Distance(this.transform.position, selectedObjects[i].transform.position) < Vector3.Distance(this.transform.position, closest.transform.position)){
            //         closest = selectedObjects[i];
            //         }
            //     }
            //     // gm.Select(selectedObjects[i]);
            // }
            // gm.Select(closest);
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





    private void OnTriggerEnter(Collider other){
        selectedObjects.Add(other.gameObject);
                    
            Debug.Log(other.gameObject.name + "added");

    }

    private void OnTriggerExit(Collider other){
        selectedObjects.Remove(other.gameObject);
    }


    
}

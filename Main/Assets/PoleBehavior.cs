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

        bool pressedAlready = false;

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
            if(!pressedAlready){
                if(gameManager.lazerSelection){



                if(gm.GetConfirmRayObject() == gm.selectedObject){//make sure ray is still pointing at object cause no leave function like pole

                gm.Select(gm.selectedObject);
                };
            }
            else if(!gameManager.lazerSelection){

            
                for(int i=0;i< selectedObjects.Count; i++){
                    // try{

                    gm.Select(selectedObjects[i]);
                    // }catch{
                    //     Debug.Log("XX tried to select " + selectedObjects[i].name);

                    // }
                }
            }
            Debug.Log("fired");
            pressedAlready = true;
            }
            
        }else{
            pressedAlready = false;
        }
    }





    private void OnTriggerEnter(Collider other){
        selectedObjects.Add(other.gameObject);
                    
            // Debug.Log(other.gameObject.name + "added");

    }

    private void OnTriggerExit(Collider other){
        selectedObjects.Remove(other.gameObject);
    }


    
}

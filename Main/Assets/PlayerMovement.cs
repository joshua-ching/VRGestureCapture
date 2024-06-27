using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;

    public InputData inp;

    public float moveSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.position += new Vector3(inp.stickL.x * Time.deltaTime * moveSpeed,0,inp.stickL.y * Time.deltaTime * moveSpeed);
    }
}


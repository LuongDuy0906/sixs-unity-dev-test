using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toward(Vector3 goalPosition)
    {
        rb.velocity = new Vector3(goalPosition.x * speed, goalPosition.y, goalPosition.z);
    }
}

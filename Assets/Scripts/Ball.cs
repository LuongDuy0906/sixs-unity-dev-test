using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public event Action BallReachedGoal;

    private Rigidbody rb;
    private ParticleSystem confettiEffect;
    
    [SerializeField] private float speed;
    [SerializeField] private GameObject confetti;
    [SerializeField] private float flightTime;
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
        Vector3 displacement = goalPosition - rb.position;
        rb.velocity = displacement / flightTime
                - 0.5f * Physics.gravity * flightTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Goal"))
        {
            confettiEffect = Instantiate(
                confetti.GetComponent<ParticleSystem>(),
                collision.GetContact(0).point,
                Quaternion.identity);

            confettiEffect.Play();
            Destroy(confettiEffect.gameObject, 5f);

            BallReachedGoal?.Invoke();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action<bool> KickAvailabilityChanged;

    [SerializeField] private float moveSpeed;
    [SerializeField] private GameManager gameManager;

    private Rigidbody rb;
    private Animator animator;
    private Ball currentBall;
    public bool CanKick => currentBall != null;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float inputVertical = Input.GetAxis("Vertical");
        float inputHorizontal = Input.GetAxis("Horizontal");
        
        rb.velocity = new Vector3(moveSpeed * inputHorizontal, rb.velocity.y, inputVertical * moveSpeed);

        animator.SetBool("isRunning",
            Mathf.Abs(inputHorizontal) > 0.1f ||
            Mathf.Abs(inputVertical) > 0.1f);

        Vector3 direction = new Vector3(inputHorizontal, 0f, inputVertical);

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.RotateTowards(
                rb.rotation, targetRotation, 540f * Time.fixedDeltaTime));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            currentBall = other.gameObject.GetComponent<Ball>();
            KickAvailabilityChanged?.Invoke(CanKick);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            currentBall = null;
            KickAvailabilityChanged?.Invoke(CanKick);
        }
    }

    public void KickBall()
    {
        gameManager.Kick(currentBall);
        currentBall = null;
        KickAvailabilityChanged?.Invoke(CanKick);
    }
}

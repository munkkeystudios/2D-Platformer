using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustController : MonoBehaviour
{

    [SerializeField] private ParticleSystem movementDust;
    [SerializeField] private ParticleSystem fallDust;
    [SerializeField] private GameObject player;
    [SerializeField] private int occurAfterVelocity = 5;
    [SerializeField] private float dustFormationPeriod = 0.5f;

    private Rigidbody2D playerRb;
    private PlayerMovementControl playerMovementControl;
    private float movementDustTimer;
    private bool wasGroundedLastFrame;
    private void Awake()
    {
        if (movementDust == null || player == null ||fallDust ==null)
        {
            Debug.LogError("Dust particle/ player obj not found");
        }
        playerRb = player.GetComponent<Rigidbody2D>();
        playerMovementControl = player.GetComponent<PlayerMovementControl>();
        if (playerRb == null || playerMovementControl == null)
        {
            Debug.LogError("rb/ movement control not found");
        }
    }
    private void Update()
    {
        movementDustTimer+= Time.deltaTime;
        bool isGrounded = playerMovementControl.isGrounded();

        if (Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity && playerMovementControl.isGrounded() )
        {
            if (movementDustTimer > dustFormationPeriod)
            {
                movementDust.Play();
                movementDustTimer = 0;
            }
        }
        if (!wasGroundedLastFrame && isGrounded)
        {
            fallDust.Play();
        }
        wasGroundedLastFrame = isGrounded;
    }
}


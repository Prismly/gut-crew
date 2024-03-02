using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class PlayerController : MonoBehaviour
{
    private BoxCollider2D myCollider;
    private Rigidbody2D myBody;

    private Vector2 currentInput = Vector2.zero;
    [Header("Running")]
    [SerializeField] private float minSpeed = 0.05f;
    [SerializeField] private float maxSpeed = 1.0f;
    [SerializeField] private float maxFallSpeed = 3.0f;
    [SerializeField] private float runAccel = 1.0f;
    [SerializeField] private float dragAccel = 1.0f;
    [Header("Jumping")]
    //private bool canJump = true;
    [SerializeField] private float jumpImpulse = 10.0f;
    [SerializeField] private float gravScaleLow = 1.0f;
    [SerializeField] private float gravScaleHigh = 2.0f;
    [SerializeField] private GroundTrigger groundChecker;
    //[SerializeField] private float groundedCastDist = 0.05f;
    [Header("Interacting")]
    [SerializeField] private GameObject InteractArea;

    private enum GroundedStates
    {
        GROUNDED,
        COYOTE_TIME,
        AIRBORNE
    }
    [SerializeField] private GroundedStates isGrounded = GroundedStates.GROUNDED;
    [SerializeField] private float coyoteTimeDur = 1.0f;
    private float coyoteTimer = 0f;

    private Vector2Int prevMoveDir = Vector2Int.zero;

    private void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        myBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // A negative value in the timer indicates that that timer is off.
        if (coyoteTimer >= 0)
        {
            coyoteTimer -= Time.deltaTime;
            if (coyoteTimer <= 0)
            {
                //Debug.Log("Grounded State to AIRBORNE (coyote timer expired)");
                isGrounded = GroundedStates.AIRBORNE;
                coyoteTimer = -1f;
            }
        }
    }

    private void FixedUpdate()
    {
        //Debug.Log("FU here. State of delayedGroundCheck is " + delayedGroundCheck);

        HorizontalMovement();

        VerticalMovement();

        myBody.velocity = new Vector2(Mathf.Clamp(myBody.velocity.x, -maxSpeed, maxSpeed),
                                        Mathf.Clamp(myBody.velocity.y, -maxFallSpeed, Mathf.Infinity));

        prevMoveDir = new Vector2Int();
        // Record current horizontal movement direction for next frame
        if (myBody.velocity.x > 0)
            prevMoveDir.x = 1;
        else if (myBody.velocity.x < 0)
            prevMoveDir.x = -1;
        else
            prevMoveDir.x = 0;
        // Record current vertical movement direction for next frame
        if (myBody.velocity.y > 0)
            prevMoveDir.y = 1;
        else if (myBody.velocity.y < 0)
            prevMoveDir.y = -1;
        else
            prevMoveDir.y = 0;
    }

    private void HorizontalMovement()
    {
        // Horizontal Drag
        if (myBody.velocity.x < 0 && currentInput.x >= 0)
            myBody.AddForce(dragAccel * Vector2.right);
        else if (myBody.velocity.x > 0 && currentInput.x <= 0)
            myBody.AddForce(dragAccel * Vector2.left);

        // Horizontal Movement Stopper
        // For cases in which the player needs to stop
        if (prevMoveDir.x > 0 && myBody.velocity.x < 0)
            myBody.velocity = new Vector2(0, myBody.velocity.y);
        if (prevMoveDir.x < 0 && myBody.velocity.x > 0)
            myBody.velocity = new Vector2(0, myBody.velocity.y);

        // Horizontal Input
        if (currentInput.x != 0)
            myBody.AddForce(runAccel * (currentInput.x > 0 ? Vector2.right : Vector2.left));
    }

    public void VerticalMovement()
    {
        // Check for Groundedness
        //groundCastFlag = true;

        bool playerOnGround = groundChecker.GroundCheck();
        if (isGrounded != GroundedStates.GROUNDED && playerOnGround && myBody.velocity.y <= 0)
        {
            // Player was not on the ground, but is now (and not traveling away from it).
            // Therefore, player is grounded.
            //Debug.Log("Grounded State to GROUNDED (ground check succeeded and falling)");
            isGrounded = GroundedStates.GROUNDED;
        }
        else if (!playerOnGround && isGrounded == GroundedStates.GROUNDED)
        {
            // Player was just on the ground, but isn't anymore.
            // Therefore, player has walked off of a ledge; activate Coyote Time(tm).
            //Debug.Log("Grounded State to COYOTE_TIME (grounded and ground check failed)");
            isGrounded = GroundedStates.COYOTE_TIME;
            coyoteTimer = coyoteTimeDur;
        }

        // Adjust Gravity Scale
        if (isGrounded != GroundedStates.AIRBORNE || myBody.velocity.y > 0)
            myBody.gravityScale = gravScaleLow;
        else
            myBody.gravityScale = gravScaleHigh;
    }

    public void Jump(float impulse = 0)
    {
        if (impulse == 0)
            impulse = jumpImpulse;
        
        // Jump Input
        //Debug.Log("Grounded State to AIRBORNE (jumped)");
        isGrounded = GroundedStates.AIRBORNE;
        coyoteTimer = -1f;
        // Zero out any vertical velocity, so falling jumps (from Coyote Time) have the same kick.
        myBody.velocity *= Vector2.right;
        myBody.AddForce(impulse * Vector2.up, ForceMode2D.Impulse);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        currentInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded != GroundedStates.AIRBORNE)
        {
            Jump();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Interact
            StartCoroutine(ToggleInteractArea());
        }
    }

    IEnumerator ToggleInteractArea()
    {
        InteractArea.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        InteractArea.SetActive(false);
    }
}

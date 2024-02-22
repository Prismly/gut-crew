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
    [SerializeField] private float runAccel = 1.0f;
    [SerializeField] private float dragAccel = 1.0f;
    [Header("Jumping")]
    private bool canJump = true;
    [SerializeField] private float jumpImpulse = 5.0f;
    [SerializeField] private float gravScaleLow = 1.0f;
    [SerializeField] private float gravScaleHigh = 2.0f;
    [SerializeField] private Transform leftToe;
    [SerializeField] private Transform rightToe;
    [SerializeField] private float groundedCastDist = 0.05f;
    private bool groundCastFlag = false;

    private enum GroundedStates
    {
        GROUNDED,
        COYOTE_TIME,
        AIRBORNE
    }
    private GroundedStates isGrounded = GroundedStates.GROUNDED;
    private float coyoteTimeDur = 1.0f;
    private float coyoteTimer = 0f;

    private Vector2Int prevMoveDir = Vector2Int.zero;

    // Directions Convention is UDLR
    private int[] contactCount = new int[4];

    private void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        myBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // The raycasts for the ground check MUST be done in Update, and not FixedUpdate.
        // For one reason or another, the position fields used as origins lag behind when done from FixedUpdate.
        if (groundCastFlag)
        {
            Debug.DrawRay(leftToe.position, Vector2.down * groundedCastDist, Color.red);
            RaycastHit2D groundCheckLeft = Physics2D.Raycast(leftToe.position, Vector2.down, groundedCastDist, LayerMask.GetMask("Terrain"));
            Debug.DrawRay(rightToe.position, Vector2.down * groundedCastDist, Color.red);
            RaycastHit2D groundCheckRight = Physics2D.Raycast(rightToe.position, Vector2.down, groundedCastDist, LayerMask.GetMask("Terrain"));

            bool groundCheck = groundCheckLeft.collider && groundCheckRight.collider;
            if (groundCheck)
            {
                isGrounded = GroundedStates.GROUNDED;
            }
            else if (isGrounded == GroundedStates.GROUNDED)
            {
                // Start Coyote Timer
                isGrounded = GroundedStates.COYOTE_TIME;
                coyoteTimer = 0f;
            }
            else
            {
                isGrounded = GroundedStates.AIRBORNE;
            }

            groundCastFlag = false;
        }

        // A negative value in the timer indicates that that timer is off.
        if (coyoteTimer >= 0)
        {
            coyoteTimer += Time.deltaTime;
            if (coyoteTimer >= coyoteTimeDur)
            {
                isGrounded = GroundedStates.AIRBORNE;
                coyoteTimer = -1f;
            }
        }
    }

    private void FixedUpdate()
    {
        HorizontalMovement();

        VerticalMovement();

        myBody.velocity = new Vector2(Mathf.Clamp(myBody.velocity.x, -maxSpeed, maxSpeed),
                                        Mathf.Clamp(myBody.velocity.y, -maxSpeed, maxSpeed));

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
        groundCastFlag = true;

        if (isGrounded == GroundedStates.GROUNDED)
        {
            // Raycast downwards from each bottom corner of the character's hitbox.
            // If neither ray hits a terrain object, the character begins coyote timing.
        }

        // Adjust Gravity Scale
        if (isGrounded != GroundedStates.AIRBORNE || myBody.velocity.y > 0)
            myBody.gravityScale = gravScaleLow;
        else
            myBody.gravityScale = gravScaleHigh;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Vector2 closestPoint = myCollider.ClosestPoint()
    //    Vector2 contactPos = Vector2.zero;
    //    while (contactPos != Vector2.zero)
    //    {
    //        contactPos = collision.GetContact(0).point;
    //    }
    //    bool upFromCenter = contactPos.y > myCollider.bounds.center.y;
    //}

    public void OnMove(InputAction.CallbackContext context)
    {
        currentInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && canJump)
        {
            // Jump Input
            myBody.AddForce(jumpImpulse * Vector2.up, ForceMode2D.Impulse);
            canJump = false;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        // Interact

    }
}

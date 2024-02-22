using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private BoxCollider2D myCollider;
    private Rigidbody2D myBody;

    private Vector2 currentInput = Vector2.zero;
    [SerializeField] private float minSpeed = 0.05f;
    [SerializeField] private float maxSpeed = 1.0f;
    [SerializeField] private float runAccel = 1.0f;
    [SerializeField] private float dragAccel = 1.0f;
    [SerializeField] private float jumpImpulse = 5.0f;
    private float gravScaleUp = 1.0f;
    private float gravScaleDown = 2.0f;

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

        if (myBody.velocity.y >= 0)
            myBody.gravityScale = gravScaleUp;
        else
            myBody.gravityScale = gravScaleDown;
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
        if (context.started && isGrounded != GroundedStates.AIRBORNE)
        {
            // Jump Input
            myBody.AddForce(jumpImpulse * Vector2.up, ForceMode2D.Impulse);
            isGrounded = GroundedStates.AIRBORNE;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        // Interact

    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public abstract class Bot : MonoBehaviour
{
    // Head and Torso may go unused, depending on scope.
    public enum Limbs { HEAD, TORSO, ARM_L, ARM_R, LEG_L, LEG_R }

    [SerializeField] protected GameManager gameManager;

    [SerializeField] protected Track myTrack;
    protected LimbData limbData = new LimbData();
    public float CurrentDist { get; set; }
    public float TargetDist { get; set; }

    [SerializeField] protected float moveStep = 1f;
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected float snapThreshold = 0.1f;
    [SerializeField] private Animator myAnimator;

    protected void Update()
    {
        if (CurrentDist == TargetDist)
            return;

        else if (Mathf.Abs(CurrentDist - TargetDist) <= snapThreshold)
            // Snap to target distance; we're close enough.
            CurrentDist = TargetDist;
        else
            // Advance towards the target distance at the robot's speed.
            CurrentDist = Mathf.Lerp(CurrentDist, TargetDist, Time.deltaTime * moveSpeed);

        //Debug.Log(CurrentDist);
        //Debug.Log(myTrack.GetPositionAtDist(CurrentDist));
        transform.position = myTrack.GetPositionAtDist(CurrentDist);
    }

    protected abstract void WinBehavior();

    public void ProcessInput(Limbs activated)
    {
        // We don't want to process input while a race is not in progress.
        if (!GameManager.Instance.RaceInProgress)
            return;

        // Update our LimbData with the most recent action by a player.
        // Note that the "Last Activated At" dictionary is NOT updated yet.
        limbData.MostRecentActive = activated;

        // Ask the current TrackSegment what this information means for the robot.
        // For example, "Move", "Don't Move", or "Stagger".
        MovementScheme currentMoveScheme = myTrack.GetSegmentAtDist(CurrentDist).MoveScheme;
        MovementScheme.Responses response = currentMoveScheme.ProcessLimbData(limbData, this);

        // Now, act on the response given by TrackSegment.
        if (response == MovementScheme.Responses.MOVE)
        {
            // Animate movement.
            SetTargetDistance(TargetDist + moveStep);
        }
        else if (response == MovementScheme.Responses.STAGGER)
        {
            // Animate stagger.
            SetTargetDistance(TargetDist - (moveStep / 2));
        }
        if (response == MovementScheme.Responses.WIN)
        {
            WinBehavior();
        }

        // Update the "Last Activated At" dictionary with the new input, for next input.
        limbData.LastActivatedAt[activated] = GameManager.Instance.RaceTime;
    }

    public void SetTargetDistance(float newVal)
    {
        newVal = Mathf.Clamp(newVal, 0, myTrack.TrackLength);
        TargetDist = newVal;
    }

    public Animator GetAnimator()
    {
        return myAnimator;
    }
}

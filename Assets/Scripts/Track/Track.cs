using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using static Level;

public class Track : MonoBehaviour
{
    // The list of points that are the path the assigned bot will follow
    public List<TrackSegment> TrackSegments { get; set; } = new List<TrackSegment>();
    public float TrackLength = 0f;
    [SerializeField] private Color debugPointColor = Color.white;

    public TrackSegment GetSegmentAtDist(float targetDist)
    {
        if (targetDist < 0f || targetDist > TrackLength || TrackSegments.Count == 0)
            return null;

        if (targetDist == 0)
            return TrackSegments[0];

        // Binary search would probably be faster; I don't expect these lists to get very
        // long though, so for simplicity I'm searching linearly
        int seg = 0;

        while (seg < TrackSegments.Count
            && TrackSegments[seg].StartDist <= targetDist)
            seg++;

        return TrackSegments[seg - 1];
    }

    public Vector3 GetPositionAtDist(float targetDist)
    {
        Debug.Log(targetDist);
        TrackSegment segment = GetSegmentAtDist(targetDist);

        float inVal = targetDist - segment.StartDist;
        float ratio = inVal / segment.Length;

        return Vector3.Lerp(segment.StartPoint, segment.EndPoint, ratio);
    }

    public void AppendSegment(float length, MovementScheme moveScheme)
    {
        Vector2 prevEndPoint;
        if (TrackSegments.Count > 0)
            prevEndPoint = TrackSegments[TrackSegments.Count - 1].EndPoint;
        else
            prevEndPoint = transform.position;

        TrackSegment appended = new TrackSegment(prevEndPoint, TrackLength, length, moveScheme);
        TrackSegments.Add(appended);
        TrackLength += appended.Length;
    }

    private void OnDrawGizmos()
    {
        float totalDistance = 0f;
        Gizmos.color = debugPointColor;
        foreach (TrackSegment seg in TrackSegments)
        {
            Gizmos.DrawSphere(transform.position + (Vector3)(Vector2.right * totalDistance), 0.1f);
            totalDistance += seg.Length;
        }
        Gizmos.DrawSphere(transform.position + (Vector3)(Vector2.right * totalDistance), 0.1f);
    }
}

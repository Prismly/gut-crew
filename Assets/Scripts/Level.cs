using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ALL THIS CODE IS SUBJECT TO CHANGE BASED ON HOW WE IMPLEMENT
// THIS IS MORESO A PROOF OF CONCEPT FOR A SYSTEM
public class Level : MonoBehaviour
{
    public enum ObstacleType
    {
        WALK,
        CLIMB,
        JUMP,
        LIFT,
        SWIM
    }

    // Node class for specifying any state we want the path to have
    public class Node
    {
        Vector2 position;
        ObstacleType obsType;

        public Node(Vector2 position, ObstacleType obsType)
        {
            this.position = position;
            this.obsType = obsType;
        }
    }

    // The list of points that are the path the player will follow
    private List<Node> path;

    // By convention, [0] will be the player and [1] will be the computer
    [SerializeField] private Vector2[] botOffs;
    [SerializeField] private Bot[] bots;
    [SerializeField] private GameObject botPref;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform c in transform)
        {
            path.Add(new Node(c.position, ObstacleType.WALK));
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}

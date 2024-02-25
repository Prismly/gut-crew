using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ALL THIS CODE IS SUBJECT TO CHANGE BASED ON HOW WE IMPLEMENT
// THIS IS MORESO A PROOF OF CONCEPT FOR A SYSTEM
public class Level : MonoBehaviour
{
    // The list of points that are the path the player will follow
    List<Node> path;

    // References to the legs/arms themselves
    [SerializeField] GameObject left;
    [SerializeField] GameObject right;

    // the indexes of each foot/arm in the node list
    int leftIdx;
    int rightIdx;

    // Start is called before the first frame update
    void Start()
    {
        leftIdx = 0;
        rightIdx = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Node class for specifying any state we want the path to have
    public class Node
    {
        Vector3 position;
    }
}

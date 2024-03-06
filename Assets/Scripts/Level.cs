using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton responsible for generating the common terrain for a leg of the race.
// Then populates the bots' respective "Track" objects so they can be manipulated individually.
public class Level : MonoBehaviour
{
    [SerializeField] private Track[] tracks;
    [SerializeField] private MovementScheme[] movementSchemes;
    [SerializeField] private int segmentCount = 3;

    [SerializeField] private Transform terrainTileContainer;

    void Start()
    {
        float totalDist = 0;
        for (int i = 0; i < segmentCount; i++)
        {
            Vector2 prevEndPoint = Vector2.zero;
            MovementScheme randScheme = movementSchemes[Random.Range(0, movementSchemes.Length)];
            int randDist = Random.Range(1, 4) * 4;
            
            for (int j = 0; j < randDist; j += 4)
            {
                GameObject newEnvironmentTile = Instantiate(randScheme.environment);
                newEnvironmentTile.transform.SetParent(terrainTileContainer);
                newEnvironmentTile.transform.localPosition = Vector3.right * (totalDist + j + 2);
            }

            totalDist += randDist;
            foreach (Track t in tracks)
            {
                t.AppendSegment(randDist, randScheme);
            }
        }
    }

    void Update()
    {

    }
}

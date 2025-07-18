using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    [SerializeField]
    private int iterations = 10;
    [SerializeField]
    public int walkLenght = 10;
    [SerializeField]
    public bool startRandomlyEachIteration = true;

    public void RunProceduralGeneration()
    {
        HashSet<Vector2Int> groundPositions = RunRandomWalk();
        foreach (var position in groundPositions)
        {
            Debug.Log(position);
        }
    }

    private HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> groundPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < iterations; i++)
        {
            var path = ProceduralDungeounAlgorithm.SimpleRandomWalk(currentPosition, walkLenght);
            groundPositions.UnionWith(path);
            if (startRandomlyEachIteration)
                currentPosition = groundPositions.ElementAt(Random.Range(0, groundPositions.Count));
        }
        return groundPositions;
    }
}

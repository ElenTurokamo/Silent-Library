using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


public class WalkerGenerator : MonoBehaviour
{
    public enum Grid
    {
        FLOOR,
        WALL,
        EMPTY
    }


    public Grid[,] gridHandler;
    public List<RandomWalker> Walkers;
    public Tilemap floorMap;
    public Tilemap wallsMap;

    //tiles
    public Tile Floor;
    public Tile WallUp;
    public Tile WallLeft;
    public Tile WallDown;
    public Tile WallRight;
    public Tile WallCornerUpRight;
    public Tile WallCornerDownRight;
    public Tile WallCornerUpLeft;
    public Tile WallCornerDownLeft;
    public Tile WallFullCornerUpRight;
    public Tile WallFullCornerDownRight;
    public Tile WallFullCornerUpLeft;
    public Tile WallFullCornerDownLeft;
    public int MapWidth = 30;
    public int MapHeight = 30;

    public int MaximumWalkers = 10;
    public int TileCount = default;
    public int LocationSize = 2000;
    public float FillPercentage = 0.4f;
    public float WaitTime = 0.05f;

    public void Generate()
    {
        InitializeGrid();
    }

    void InitializeGrid()
    {
        gridHandler = new Grid[MapWidth, MapHeight];

        for (int x = 0; x < gridHandler.GetLength(0); x++)
        {
            for (int y = 0; y < gridHandler.GetLength(1); y++)
            {
                gridHandler[x, y] = Grid.EMPTY;
            }
        }

        Walkers = new List<RandomWalker>();

        Vector3Int TileCenter = new Vector3Int(gridHandler.GetLength(0) / 2, gridHandler.GetLength(1) / 2, 0);

        RandomWalker curWalker = new RandomWalker(new Vector2(TileCenter.x, TileCenter.y), GetDirection(), 0.5f);
        gridHandler[TileCenter.x, TileCenter.y] = Grid.FLOOR;
        floorMap.SetTile(TileCenter, Floor);
        Walkers.Add(curWalker);

        TileCount++;

        StartCoroutine(CreateFloors());
    }

    Vector2 GetDirection()
    {
        int choice = (UnityEngine.Random.Range(0, 4));
        switch (choice)
        {
            case 0:
                return Vector2.down;
            case 1:
                return Vector2.left;
            case 2:
                return Vector2.up;
            case 3:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }

    IEnumerator CreateFloors()
    {
        while ((float)TileCount / (float)gridHandler.Length < FillPercentage)
        {
            bool hasCreatedFloor = false;
            foreach (RandomWalker curWalker in Walkers)
            {
                Vector3Int curPos = new Vector3Int((int)curWalker.Position.x, (int)curWalker.Position.y, 0);

                if (gridHandler[curPos.x, curPos.y] != Grid.FLOOR)
                {
                    floorMap.SetTile(curPos, Floor);
                    TileCount++;
                    gridHandler[curPos.x, curPos.y] = Grid.FLOOR;
                    hasCreatedFloor = true;
                }
            }

            ChanceToRemove();
            ChanceToRedirect();
            ChanceToCreate();
            UpdatePosition();

            if (hasCreatedFloor)
            {
                yield return new WaitForSeconds(WaitTime);
            }
        }

        StartCoroutine(CreateWalls());
    }

    void ChanceToRemove()
    {
        int updateCount = Walkers.Count;
        for (int i = 0; i < updateCount; i++)
        {
            if (UnityEngine.Random.value < Walkers[i].ChanceToChange && Walkers.Count > 1)
            {
                Walkers.RemoveAt(i);
                break;
            }
        }

    }

    void ChanceToRedirect()
    {
        for (int i = 0; i < Walkers.Count; i++)
        {
            if (UnityEngine.Random.value < Walkers[i].ChanceToChange)
            {
                RandomWalker curWalker = Walkers[i];
                curWalker.Direction = GetDirection();
                Walkers[i] = curWalker;
            }
        }
    }

    void ChanceToCreate()
    {
        int updatedCount = Walkers.Count;
        for (int i = 0; i < updatedCount; i++)
        {
            if (UnityEngine.Random.value < Walkers[i].ChanceToChange && Walkers.Count < MaximumWalkers)
            {
                Vector2 newDirection = GetDirection();
                Vector2 newPosition = Walkers[i].Position;

                RandomWalker newWalker = new RandomWalker(newPosition, newDirection, 0.5f);
                Walkers.Add(newWalker);
            }
        }
    }

    void UpdatePosition()
    {
        for (int i = 0; i < Walkers.Count; i++)
        {
            RandomWalker FoundWalker = Walkers[i];
            FoundWalker.Position += FoundWalker.Direction;
            FoundWalker.Position.x = Mathf.Clamp(FoundWalker.Position.x, 1, gridHandler.GetLength(0) - 2);
            FoundWalker.Position.y = Mathf.Clamp(FoundWalker.Position.y, 1, gridHandler.GetLength(1) - 2);
            Walkers[i] = FoundWalker;
        }
    }

    IEnumerator CreateWalls()
    {
        for (int x = 0; x < gridHandler.GetLength(0) - 1; x++)
        {
            for (int y = 0; y < gridHandler.GetLength(1) - 1; y++)
            {
                if (gridHandler[x, y] == Grid.FLOOR)
                {
                    bool hasCreatedTile = false;

                    if (gridHandler[x, y] == Grid.EMPTY)
                    {
                        if (gridHandler[x - 1, y + 1] == Grid.FLOOR
                        && gridHandler[x, y + 1] == Grid.FLOOR
                        && gridHandler[x + 1, y + 1] == Grid.FLOOR
                        && gridHandler[x + 1, y] == Grid.FLOOR
                        && gridHandler[x + 1, y - 1] == Grid.FLOOR
                        && gridHandler[x, y - 1] == Grid.FLOOR
                        && gridHandler[x - 1, y - 1] == Grid.FLOOR
                        && gridHandler[x - 1, y] == Grid.FLOOR
                        )
                        {
                            floorMap.SetTile(new Vector3Int(x, y, 0), WallCornerDownLeft);
                            gridHandler[x, y] = Grid.FLOOR;
                            hasCreatedTile = true;
                        }
                    }
                    // //closing floor holes
                    // if (gridHandler[x, y] == Grid.EMPTY)
                    // {
                    //     if (gridHandler[x, y + 1] == Grid.FLOOR && gridHandler[x, y - 1] == Grid.FLOOR)
                    //     {
                    //         floorMap.SetTile(new Vector3Int(x, y, 0), Floor);
                    //         gridHandler[x, y] = Grid.FLOOR;
                    //         hasCreatedTile = true;                            
                    //     }
                    // }
                    // if (gridHandler[x, y] == Grid.EMPTY)
                    // {
                    //     if (gridHandler[x + 1, y] == Grid.FLOOR && gridHandler[x - 1, y] == Grid.FLOOR)
                    //     {
                    //         floorMap.SetTile(new Vector3Int(x, y, 0), Floor);
                    //         gridHandler[x, y] = Grid.FLOOR;
                    //         hasCreatedTile = true;                            
                    //     }
                    // }

                    // //corners generator
                    // if (gridHandler[x + 1, y + 1] == Grid.EMPTY)
                    // {
                    //     if ((gridHandler[x, y + 1] == Grid.WALL) && (gridHandler[x + 1, y] == Grid.WALL))
                    //     {
                    //         wallsMap.SetTile(new Vector3Int(x + 1, y + 1, 0), WallCornerUpRight);
                    //         gridHandler[x + 1, y + 1] = Grid.WALL;
                    //         hasCreatedTile = true;
                    //     }
                    // }
                    // if (gridHandler[x + 1, y - 1] == Grid.EMPTY)
                    // {
                    //     if ((gridHandler[x + 1, y] == Grid.WALL) && (gridHandler[x, y - 1] == Grid.WALL))
                    //     {
                    //         wallsMap.SetTile(new Vector3Int(x + 1, y - 1, 0), WallCornerDownRight);
                    //         gridHandler[x + 1, y - 1] = Grid.WALL;
                    //         hasCreatedTile = true;
                    //     }
                    // }
                    // if (gridHandler[x - 1, y + 1] == Grid.EMPTY)
                    // {
                    //     if ((gridHandler[x - 1, y] == Grid.WALL) && (gridHandler[x, y + 1] == Grid.WALL))
                    //     {
                    //         wallsMap.SetTile(new Vector3Int(x - 1, y + 1, 0), WallCornerUpLeft);
                    //         gridHandler[x - 1, y + 1] = Grid.WALL;
                    //         hasCreatedTile = true;
                    //     }
                    // }
                    // if (gridHandler[x - 1, y - 1] == Grid.EMPTY)
                    // {
                    //     if ((gridHandler[x - 1, y] == Grid.WALL) && (gridHandler[x, y - 1] == Grid.WALL))
                    //     {
                    //         wallsMap.SetTile(new Vector3Int(x - 1, y - 1, 0), WallCornerDownLeft);
                    //         gridHandler[x - 1, y - 1] = Grid.WALL;
                    //         hasCreatedTile = true;
                    //     }
                    // }

                    // //full corners generator
                    // if (gridHandler[x - 1, y] == Grid.EMPTY)
                    // {
                    //     if ((gridHandler[x, y + 1] == Grid.FLOOR) && (gridHandler[x - 1, y + 1] == Grid.FLOOR))
                    //     {
                    //         wallsMap.SetTile(new Vector3Int(x - 1, y, 0), WallFullCornerUpRight);
                    //         gridHandler[x - 1, y] = Grid.WALL;
                    //         hasCreatedTile = true;
                    //     }
                    // }

                    // //direction walls generator
                    // if (gridHandler[x + 1, y] == Grid.EMPTY)
                    // {
                    //     wallsMap.SetTile(new Vector3Int(x + 1, y, 0), WallRight);
                    //     gridHandler[x + 1, y] = Grid.WALL;
                    //     hasCreatedTile = true;
                    // }
                    // if (gridHandler[x - 1, y] == Grid.EMPTY)
                    // {
                    //     wallsMap.SetTile(new Vector3Int(x - 1, y, 0), WallLeft);
                    //     gridHandler[x - 1, y] = Grid.WALL;
                    //     hasCreatedTile = true;
                    // }
                    // if (gridHandler[x, y + 1] == Grid.EMPTY)
                    // {
                    //     wallsMap.SetTile(new Vector3Int(x, y + 1, 0), WallUp);
                    //     gridHandler[x, y + 1] = Grid.WALL;
                    //     hasCreatedTile = true;
                    // }
                    // if (gridHandler[x, y - 1] == Grid.EMPTY)
                    // {
                    //     wallsMap.SetTile(new Vector3Int(x, y - 1, 0), WallDown);
                    //     gridHandler[x, y - 1] = Grid.WALL;
                    //     hasCreatedTile = true;
                    // }                    

                    if (hasCreatedTile)
                    {
                        yield return new WaitForSeconds(WaitTime);
                    }
                }
            }
        }
    }

}

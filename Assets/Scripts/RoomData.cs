using UnityEngine;

public class RoomData
{
    public Vector2Int gridPositions;
    public GameObject roomObject;
    public bool isStartRoom = false;
    public bool isEndRoom = false;

    public RoomData(Vector2Int pos)
    {
        gridPositions = pos;
    }
}

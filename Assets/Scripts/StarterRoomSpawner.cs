using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarterRoomSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject startingRoomPrefab;
    void Start()
    {
        // Instantiate(startingRoomPrefab, transform.position, Quaternion.identity);
        GameObject playerObj = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        GameObject.FindAnyObjectByType<CameraFollow>().player = playerObj.transform;
    }
}

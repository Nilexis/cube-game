using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private Transform playerTransform;
    private float spawnZ = 0.0f;
    private float tileLen = 40.0f;
    private int tileAmountFront = 8;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for(int i = 0; i < tileAmountFront; i++)
            SpawnTile();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.z > (spawnZ - tileAmountFront * tileLen))
            SpawnTile();
    }

    //spawn a tile
    private void SpawnTile(int prefabNumber = -1)
    {
        GameObject tile;
        tile = Instantiate(tilePrefabs [0]) as GameObject;
        tile.transform.SetParent(transform);
        tile.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLen;
    }
}

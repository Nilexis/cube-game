using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private Transform playerTransform;
    private float spawnZ = -40.0f;
    private float tileLen = 40.0f;
    private int tileAmountFront = 15;
    private int lastPrefabIdx = 0;
    private int spaceTile = 0;
    private List<GameObject> activeTiles;

    // Start is called before the first frame update
    void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < tileAmountFront; i++)
        {
            if (i < 8)
                SpawnTile(0);
            else
            {
                if (spaceTile != 0)
                    SpawnTile(0);
                else
                    SpawnTile();
                spaceTile = (spaceTile+1)%3;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - 2*tileLen > (spawnZ - tileAmountFront * tileLen))
        {
            DeleteTile();
            if (spaceTile != 0)
                SpawnTile(0);
            else
                SpawnTile();
            spaceTile = (spaceTile + 1) % 3;
        }
    }

    //spawn a tile
    private void SpawnTile(int prefabNumber = -1)
    {
        GameObject tile;
        if(prefabNumber == -1)
            tile = Instantiate( tilePrefabs[ GetRndIdx() ] ) as GameObject;
        else
            tile = Instantiate(tilePrefabs[prefabNumber]) as GameObject;
        tile.transform.SetParent(transform);
        tile.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLen;
        activeTiles.Add(tile);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int GetRndIdx()
    {
        if (tilePrefabs.Length < 2)
            return 0;
        int rndIdx = lastPrefabIdx;
        while (rndIdx == lastPrefabIdx)
            rndIdx = Random.Range(0, tilePrefabs.Length);

        lastPrefabIdx = rndIdx;
        return rndIdx;
    }

}

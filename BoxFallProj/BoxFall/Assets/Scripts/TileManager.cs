using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject[] treePrefabs;
    private Transform playerTransform;
    private float tileLen = 150.0f;
    private float spawnZ = -150.0f;
    private int tileAmountFront = 8;
    private int lastPrefabIdx = 0;
    private int tileWidth = 5;
    private int treesInTileRow = 5;
    private List<GameObject> activeTiles;
    private List<GameObject> activeTrees;


    // Start is called before the first frame update
    void Start()
    {
        spawnZ = -1 * tileLen;
        activeTiles = new List<GameObject>();
        activeTrees = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < tileAmountFront; i++)
        {
            if (i < 2)
                SpawnTile(0);
            else
            {
                SpawnTile();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - 3 * tileLen > (spawnZ - tileAmountFront * tileLen))
        {
            DeleteTile();
            SpawnTile();
        }
    }

    //spawn a tile
    private void SpawnTile(int prefabNumber = -1)
    {
        GameObject tile;
        GameObject tree;

        for (int i = 0; i < tileWidth; i++)
        {
            tile = Instantiate(tilePrefabs[0]) as GameObject;
            tile.transform.SetParent(transform);
            tile.transform.position = new Vector3(-((tileWidth - 1)*tileLen)/2 + i*tileLen, 0, spawnZ);
            activeTiles.Add(tile);
            if (prefabNumber != 0)
            {
                //spawn trees
                float x, y, z;
                for (int x2 = -(((int)tileWidth - 1) * (int)tileLen) / 2 + i * (int)tileLen - (int)tileLen / 2; x2 < -((tileWidth - 1) * tileLen) / 2 + i * tileLen + (int)tileLen / 2; x2 += (int)tileLen / treesInTileRow)
                {
                    for (int z2 = (int)spawnZ; z2 < spawnZ + (int)tileLen; z2 += (int)tileLen / treesInTileRow)
                    {
                        x = x2 + Random.Range(0, tileLen / treesInTileRow);
                        z = z2 + Random.Range(0, tileLen / treesInTileRow);
                        tree = Instantiate(treePrefabs[GetRndIdxTrees()]) as GameObject;
                        y = Mathf.PerlinNoise(x * .06f, z * .1f) * 4.0f - z * 1.5f + Mathf.PerlinNoise(x * .002f, z * .01f) * (-10);
                        tree.transform.SetParent(transform);
                        tree.transform.position = new Vector3(x, y, z);
                        activeTrees.Add(tree);
                    }
                }
            }
        }
        
        spawnZ += tileLen;
    }

    private void DeleteTile()
    {
        for(int i = 0; i < treesInTileRow * treesInTileRow; i++)
        {
            Destroy(activeTrees[0]);
            activeTrees.RemoveAt(0);
        }
        for (int i = 0; i < tileWidth; i++)
        {
            Destroy(activeTiles[0]);
            activeTiles.RemoveAt(0);
        }
    }

    private int GetRndIdxTrees()
    {
        if (treePrefabs.Length < 2)
            return 0;
        int rndIdx = lastPrefabIdx;
        while (rndIdx == lastPrefabIdx)
            rndIdx = Random.Range(0, treePrefabs.Length);

        lastPrefabIdx = rndIdx;
        return rndIdx;
    }

}

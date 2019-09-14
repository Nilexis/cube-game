using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject[] obstPrefabs;
    private Transform playerTransform;
    public static float tileLen = 100f;
    public static float tileWidth = 200f;
    private float tileAngle = 30f;
    private float spawnZ = 0f;
    private int tileAmountFront = 6;
    private int lastPrefabIdx = 0;
    private int treesInTileRow = 2;
    private int bushesInTileRow = 0;
    private int obstDis;
    private List<int> tileTags;
    private List<GameObject> activeTiles;
    private List<GameObject> activeObstacles;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnZ = -1;
        activeTiles = new List<GameObject>();
        activeObstacles = new List<GameObject>();
        tileTags = new List<int>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < tileAmountFront; i++)
        {
            if (i < 3)
                SpawnTile(0);
            else
            {
                SpawnTile();
            }
        }
        obstDis = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - 3 * Mathf.Cos(tileAngle * Mathf.PI / 180.0f) * tileLen > (spawnZ - tileAmountFront) * Mathf.Cos(tileAngle * Mathf.PI / 180.0f) * tileLen)
        {
            DeleteTile();
            if(obstDis % 2 == 0)
                SpawnTile(0);
            else
                SpawnTile();
            obstDis = obstDis % 2 + 1;
        }
    }

    //spawn a tile
    private void SpawnTile(int prefabNumber = -1)
    {
        GameObject tile;
        GameObject obstacle;
        int obsIdx;
        
        tile = Instantiate(tilePrefabs[0]) as GameObject;
        tile.transform.SetParent(transform);
        tile.transform.position = new Vector3(0, -Mathf.Sin(tileAngle*Mathf.PI/180.0f) * tileLen * spawnZ, Mathf.Cos(tileAngle * Mathf.PI / 180.0f) * tileLen * spawnZ);
        activeTiles.Add(tile);

        if(prefabNumber == -1)
        {
            obsIdx = GetRndIdxObstacles();
        }
        else
        {
            obsIdx = prefabNumber;
        }

        tileTags.Add(obsIdx);

        obstacle = Instantiate(obstPrefabs[obsIdx]) as GameObject;
        obstacle.transform.SetParent(transform);
        obstacle.transform.position = new Vector3(0, -Mathf.Sin(tileAngle * Mathf.PI / 180.0f) * tileLen * spawnZ, Mathf.Cos(tileAngle * Mathf.PI / 180.0f) * tileLen * spawnZ);
        activeObstacles.Add(obstacle);

        spawnZ += 1;
    }

    private void DeleteTile()
    {
        //delete meshes
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
        //delete obstacles
        Destroy(activeObstacles[0]);
        activeObstacles.RemoveAt(0);

        tileTags.RemoveAt(0);
    }

    private int GetRndIdxObstacles()
    {
        if (obstPrefabs.Length < 2)
            return 0;
        int rndIdx = lastPrefabIdx;
        while (rndIdx == lastPrefabIdx)
            rndIdx = Random.Range(1, obstPrefabs.Length);

        lastPrefabIdx = rndIdx;
        return rndIdx;
    }

}

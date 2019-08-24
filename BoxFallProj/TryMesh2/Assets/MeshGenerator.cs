using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;

    public int xSize = 150;
    public int zSize = 200;
    private int startOffset = 50;
    public float WHAPERLIN = 2f;
    public float WHAPERLIN2 = .7f;
    private float textureSize = 5f;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        CreateShape();
        UpdateMesh();

        for(int k = 0; k < triangles.Length; k += 3)
        {
            Debug.Log(triangles[k] + " " + triangles[k + 1] + " " + triangles[k + 2] + "\n");
        }

    }

    void CreateShape()
    {
        if (xSize % 2 != 0)
            xSize++;
        if (zSize % 2 != 0)
            zSize++;

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0 - startOffset; z <= zSize - startOffset; z++)
        {
            for (int x = -xSize/2; x <= xSize/2; x++)
            {
                float y = Mathf.PerlinNoise(x * .15f, z * .15f) * .7f - (float) z / 3 + Mathf.PerlinNoise(0,z * .01f) * (-10);
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

        uvs = new Vector2[vertices.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                uvs[i] = new Vector2((float)x / xSize * textureSize, (float)z / xSize * textureSize);
                i++;
            }
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
   

    // Update is called once per frame
    void Update()
    {
        //CreateShape();
        //UpdateMesh();
    }
}

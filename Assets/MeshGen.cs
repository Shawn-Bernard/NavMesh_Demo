using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGen : MonoBehaviour
{
    Mesh mesh;

    int height = 30;
    int width = 30;

    Vector3[] verts;

    int pos = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = new Mesh();

        verts = new Vector3[(width + 1) * (height + 1)];
        int[] tris = new int[width * height * 6];
        Vector2[] uvs = new Vector2[verts.Length];

        int tri = 0;

        int vert = 0;
        for (int i = 0; i < height + 1; i++)
        {
            for (int j = 0; j < width + 1; j++)
            {
                float xCoord = (float)j / width;
                float yCoord = (float)j / height;
                float noise = Mathf.Clamp((Mathf.PerlinNoise(xCoord, yCoord) -.5f) * 5,-1,1);

                verts[pos] = new Vector3 (j,noise,i);

                uvs[pos] = new Vector2((float)j/width,(float)i/height);



                pos++;
            }
        }


        

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                // First triangle
                tris[tri + 0] = vert + 0;
                tris[tri + 1] = vert + width + 1;
                tris[tri + 2] = vert + 1;

                tris[tri + 3] = vert + 1;
                tris[tri + 4] = vert + width + 1;
                tris[tri + 5] = vert + width + 2;

                tri += 6;
                vert++;
            }
            vert++;
        }


        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;


    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying == false) return;

        for (int i = 0; i < verts.Length; i++)
        {
            Gizmos.DrawSphere(verts[i], .10f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

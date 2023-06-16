using UnityEngine;

public class TerrainBuilder : MonoBehaviour
{
    public int Size;
    [HideInInspector] public Vector3 playerPosition;
    Vector2 offset;
    
    Mesh mesh;
    int[] triangles;
    Vector3[] vertices;
    Vector2[] noisemodifiers,
        uvs;

    void Start()
    {
        offset = Vector2.one * Size;
        Size *= 2;

        CreateModifiers();
        BuildMesh();
        gameObject.AddComponent<MeshCollider>();
        Physics.Raycast(Vector3.up * 20, Vector3.down, out RaycastHit hit);
        playerPosition = hit.point + Vector3.up * 1.5f;
        GameObject.FindGameObjectWithTag("Player").transform.position = playerPosition;
        GameObject.FindGameObjectWithTag("Particles").transform.position = playerPosition;
        gameObject.GetComponent<TerrainFiller>().Fill();
    }

    void CreateModifiers()
    {
        noisemodifiers = new Vector2[4];
        //0 - оффсет шума, 1 - коэфициент высоты точки шума первого уровня, 2 и 3 - коэфициенты шумов второго и третьего уровня
        noisemodifiers[0] = new Vector2(Random.Range(0, 101), Random.Range(0, 101));
        noisemodifiers[1] = new Vector2(Random.Range(10, 15), 0);
        noisemodifiers[2] = new Vector2(Random.Range(20, 30) * .001f, Random.Range(20, 30) * .001f);
        float rnd = Random.Range(80, 101) * .01f;
        noisemodifiers[3] = new Vector2(rnd, rnd);
    }
    
    float CreateDiversity(int x, int z)
    {
        return Mathf.PerlinNoise(x * .01f + noisemodifiers[0].x, z * .01f + noisemodifiers[0].y) * noisemodifiers[1].x +
                Mathf.PerlinNoise(x * noisemodifiers[2].x + noisemodifiers[0].x, z * noisemodifiers[2].y + noisemodifiers[0].y) * 2 +
                Mathf.PerlinNoise(x * noisemodifiers[3].x + noisemodifiers[0].x, z * noisemodifiers[3].y + noisemodifiers[0].y) * .75f;
    }
    
    void BuildMesh()
    {
        vertices = new Vector3[(Size + 1) * (Size + 1)];
        for (int i = 0, z = 0; z <= Size; z++)
        {
            for (int x = 0; x <= Size; x++)
            {
                if (Vector2.Distance(Vector2.zero, new Vector2(x - offset.x, z - offset.y)) - .5f <= offset.x)
                    vertices[i] = new Vector3(x - offset.x, CreateDiversity(x, z), z - offset.y);
                else vertices[i] = new Vector3(0, -16, 0);
                i++;
            }
        }

        triangles = new int[Size * Size * 6];
        int verts = 0, tris = 0;
        for (int z = 0; z < Size; z++)
        {
            for (int x = 0; x < Size; x++)
            {
                triangles[tris + 0] = verts + 0;
                triangles[tris + 1] = verts + Size + 1;
                triangles[tris + 2] = verts + 1;
                triangles[tris + 3] = verts + 1;
                triangles[tris + 4] = verts + Size + 1;
                triangles[tris + 5] = verts + Size + 2;

                verts++;
                tris += 6;
            }
            verts++;
        }
        
        uvs = new Vector2[vertices.Length];
        for (int i = 0, z = 0; z <= Size; z++)
        {
            for (int x = 0; x <= Size; x++)
            {
                uvs[i] = new Vector2((float)x / Size, (float)z / Size);
                i++;
            }
        }

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }
}
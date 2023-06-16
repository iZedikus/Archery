using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerrainFiller : MonoBehaviour
{
    [SerializeField] GameObject[] treeVariants, bushVariants, stoneVariants, grassVariants;
    Transform trees, bushes, stones, grass;

    void Start()
    {
        trees = GameObject.FindGameObjectWithTag("Trees").transform;
        bushes = GameObject.FindGameObjectWithTag("Bushes").transform;
        stones = GameObject.FindGameObjectWithTag("Stones").transform;
        grass = GameObject.FindGameObjectWithTag("Grass").transform;
    }

    public void Fill()
    {
        Vector3[] points = GeneratePointField();
        List<Vector3> readyPoints = new List<Vector3>();
        for (int i = points.Length - 1; i >= 0; i--)
        {
            points[i].y *= 4;
            float dist = points[i].DistanceToClosestObject(readyPoints.ToArray());
            if (dist > 0 || (dist < 0 && -dist < points[i].y))
            {
                if(dist < 0 && -dist < points[i].y) points[i].y += dist;
                float scale = points[i].y / 4;
                
                if (scale.isBetween(.5f, .55f) || scale.isBetween(.6f, .65f)) Instantiate(bushVariants[Random.Range(0, bushVariants.Length)], new Vector3(points[i].x, 20, points[i].z), Quaternion.identity, bushes).transform.localScale *= Random.Range(.6f, .8f);
                else if (scale.isBetween(.7f, .75f) || scale.isBetween(.8f, .85f)) Instantiate(stoneVariants[Random.Range(0, stoneVariants.Length)], new Vector3(points[i].x, 20, points[i].z), Quaternion.identity, stones).transform.localScale *= Random.Range(1f, 1.5f);
                else if (scale.isBetween(.9f, 1)) Instantiate(treeVariants[Random.Range(0, treeVariants.Length)], new Vector3(points[i].x, 20, points[i].z), Quaternion.identity, trees).transform.localScale *= Random.Range(1.75f, 2.25f);
                else if (Random.Range(0, 3) < 2) Instantiate(grassVariants[Random.Range(0, grassVariants.Length)], new Vector3(points[i].x, 20, points[i].z), Quaternion.identity, grass).transform.localScale *= Random.Range(.75f, 1.25f);

                readyPoints.Add(points[i]);
            }
        }
    }

    Vector3[] GeneratePointField()
    {
        List<Vector3> spawnPoints = new List<Vector3>();
        float size = GetComponent<TerrainBuilder>().Size;
        float angle = 0, dist = 3, addDist = .1f;
        Vector3 noiseModifier = new Vector2(Random.Range(0, 101), Random.Range(0, 101));
        while (dist < size * .5f)
        {
            float y = dist * Mathf.Sin(angle),
                x = dist * Mathf.Cos(angle);
            spawnPoints.Add(new Vector3(x, Mathf.Clamp(Mathf.Sin(PerlinNoise(new Vector2(x, y), noiseModifier) * Mathf.PI * .5f), 0f, dist * .1f), y));
            angle += 1.001f;
            dist += addDist;
        }
        return spawnPoints.OrderBy(v => v.y).ToArray();
    }

    float PerlinNoise(Vector2 pos, Vector2 mod)
    {
        return Mathf.PerlinNoise(pos.x * .03f + mod.x, pos.y * .03f + mod.y) + Mathf.Pow(Mathf.PerlinNoise(pos.x * .5f + mod.x, pos.y * .5f + mod.y), 2);
    }
}
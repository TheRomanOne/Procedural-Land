using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Params : MonoBehaviour
{
    private Mesh mesh;
    private MeshFilter mf;
    private Vector3[] vertices;
    private float[] pos = new float[] { 1000.254f, 1500.2598f };

    public int MAX_COUNT;

    public float hMultiplier = 700;
    public float freq = 700;
    public float perlinDamp = 5000;
    public bool desert = false;

    public float perlinOffset, perlinAmp1, perlinAmp2, perlinAmp3, perlinDensity1, perlinDensity2, perlinDensity3;

    void Start ()
    {
        mf = GetComponent<MeshFilter>();
        mesh = mf.mesh;
        vertices = mesh.vertices;
        perlinDamp = 5000;

        perlinOffset = 0;
        perlinAmp1 = 29.26f;
        perlinAmp2 = 6.9f;
        perlinAmp3 = 5.4f;

        perlinDensity1 = 18.29f;
        perlinDensity2 = 9.11f;
        perlinDensity3 = 5.31f;
    }

    public void setAttributes(float perlinDamp, float perlinOffset, float perlinAmp1, float perlinAmp2, float perlinAmp3, float perlinDensity1, float perlinDensity2, float perlinDensity3)
    {
        this.perlinDamp = perlinDamp;

        this.perlinOffset = perlinOffset;
        this.perlinAmp1 = perlinAmp1;
        this.perlinAmp2 = perlinAmp2;
        this.perlinAmp3 = perlinAmp3;

        this.perlinDensity1 = perlinDensity1;
        this.perlinDensity2 = perlinDensity2;
        this.perlinDensity3 = perlinDensity3;
    }

    float getFloat(float num)
    {
        return freq * num / perlinDamp;
    }

    public void updateMesh ()
    {
        Vector3[] vs = new Vector3[vertices.Length];
        Vector3[,] mat = getMatrix(vertices, MAX_COUNT);
        int i = 0;
        
        for (int j = 0; j < mat.GetLength(0); j++)
            for (int k = 0; k < mat.GetLength(1); k++, i++)
            {
                float perlin = 0;

                perlin = perlinOffset + perlinAmp1 * Mathf.PerlinNoise(getFloat(j + pos[0] + 5) * perlinDensity1 / 100f, getFloat(k + pos[1]) * perlinDensity1 / 100f) / 100f;
                perlin += perlinAmp2 * Mathf.PerlinNoise(getFloat(j + pos[0] + 5) * perlinDensity2 / 10f, getFloat(k + pos[1]) * perlinDensity2 / 10f) / 100f;
                perlin += perlinAmp3 * Mathf.PerlinNoise(getFloat(j + pos[0] + 5) * perlinDensity3 / 10f, getFloat(k + pos[1]) * perlinDensity3 / 10f) / 100f;

                vs[i] = vertices[i];

                vs[i].y = hMultiplier * perlin;
            }

        mesh.vertices = vs;
        mf.mesh = mesh;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
    }

    Vector3[,] getMatrix(Vector3[] vertices, int MAX_COUNT)
    {
        Vector3[,] mat = new Vector3[MAX_COUNT, MAX_COUNT];

        for (int i = 0; i < MAX_COUNT; i++)
            for (int j = 0; j < MAX_COUNT; j++)
                mat[i, j] = vertices[i * MAX_COUNT + j];

        return mat;
    }
}

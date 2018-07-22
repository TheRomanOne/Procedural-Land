using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    private float interval;
    private int maxSize;
    private List<Vector3> coordinates;

    public  GameObject createObject(string name, Material mat, float interval, int maxSize)
    {
        this.interval = interval;
        this.maxSize = maxSize;

        GameObject entity = new GameObject(name);

        MeshFilter mf = entity.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer mr = entity.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

        Mesh m = new Mesh();

        generateTriangles(m);

        mf.mesh = m;
        mr.material = mat;

        entity.AddComponent(typeof(Params));
        entity.GetComponent<Params>().MAX_COUNT = maxSize;

        entity.AddComponent(typeof(Rigidbody));
        entity.GetComponent<Rigidbody>().useGravity = false;
        entity.GetComponent<Rigidbody>().isKinematic = true;

        entity.AddComponent<MeshCollider>();

        return entity;
    }

    private void generateTriangles(Mesh mesh)
    {
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();
        List<int> indices = new List<int>();

        for (int j = 0; j < maxSize; j++)
            for (int i = 0; i < maxSize; i++)
            {
                Vector3 v = new Vector3(i * interval, 0, j * interval);

                vertices.Add(v);

                if (i > 0 && j > 0)
                {
                    int offset = vertices.Count - maxSize - 2;
                    int hook = vertices.Count - 1;

                    indices.Add(offset);
                    indices.Add(offset + maxSize);
                    indices.Add(hook);

                    indices.Add(offset);
                    indices.Add(hook);
                    indices.Add(hook - maxSize);

                }
            }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = indices.ToArray();

        mesh.uv = uv.ToArray();
    }
}

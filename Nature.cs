using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nature : MonoBehaviour
{
    public Material terrainMat;

    GameObject terrain;

    public bool updateCollider = false;
    public bool refresh = true;
    public bool run = false;
    public int force;
    public float terrainInterval;
    public int terrainMaxCount;

    private Transform sphere;
    private Transform cam;

    void Start()
    {
        Entity entity = new Entity();

        terrain = entity.createObject("Terrain", terrainMat, terrainInterval, terrainMaxCount);

        sphere = GameObject.Find("Sphere").transform;
        cam = GameObject.Find("Main Camera").transform;

        sphere.position = terrain.transform.position + new Vector3(30, 135, 30);

    }

    void Update()
    {
        cam.position = sphere.position + new Vector3(0, 6, -12);
        cam.LookAt(cam);
        if(refresh || run)
        {
            terrain.GetComponent<Params>().updateMesh();
            refresh = false;
        }

        if(updateCollider)
        {
            terrain.GetComponent<MeshCollider>().sharedMesh = terrain.GetComponent<MeshFilter>().mesh;
            updateCollider = false;
        }

        if(Input.GetKey("p"))
        {
            updateCollider = true;
            sphere.GetComponent<Rigidbody>().isKinematic = false;
        }

        if(Input.GetKey("w"))
        {
            sphere.GetComponent<Rigidbody>().AddForce(Vector3.forward * force);
        }

        if (Input.GetKey("d"))
        {
            sphere.GetComponent<Rigidbody>().AddForce(Vector3.right * force);
        }

        if (Input.GetKey("a"))
        {
            sphere.GetComponent<Rigidbody>().AddForce(Vector3.right * -force);
        }

        if (Input.GetKey("s"))
        {
            sphere.GetComponent<Rigidbody>().AddForce(Vector3.forward * -force);
        }

        if (Input.GetKey("space"))
        {
            sphere.GetComponent<Rigidbody>().AddForce(Vector3.up * force * 3);
        }

        if (Input.GetKey("left ctrl"))
        {
            sphere.GetComponent<Rigidbody>().AddForce(Vector3.up * force * -3);
        }


    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollisionManager : MonoBehaviour
{
    public List<GameObject> Cubes;
    public List<GameObject> Spheres;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < Cubes.Count; i++)
        {
            for(int j = 0; j < Cubes.Count; j++)
            {
                if (i != j)
                {
                    if (AABBCheck(Cubes[i], Cubes[j]))
                    {
                        Debug.Log("Colliding");
                    }
                }
            }
            for(int k = 0; k < Spheres.Count; k++)
            {
                if (SphereAABBCheck(Spheres[k], Cubes[i]))
                {
                    Debug.Log("Colliding Sphere");
                    Destroy(Spheres[k]);
                    Spheres.RemoveAt(k);
                }

            }
        }
    }

    private bool AABBCheck(GameObject a, GameObject b)
    {
        MeshFilter aMF = a.GetComponent<MeshFilter>();
        MeshFilter bMF = b.GetComponent<MeshFilter>();

        Bounds aB = aMF.mesh.bounds;
        Bounds bB = bMF.mesh.bounds;

        var min1 = Vector3.Scale(aB.min, a.transform.localScale) + a.transform.position;
        var max1 = Vector3.Scale(aB.max, a.transform.localScale) + a.transform.position;

        var min2 = Vector3.Scale(bB.min, b.transform.localScale) + b.transform.position;
        var max2 = Vector3.Scale(bB.max, b.transform.localScale) + b.transform.position;

        if ((min1.x <= max2.x && max1.x >= min2.x) &&
            (min1.y <= max2.y && max1.y >= min2.y) &&
            (min1.z <= max2.z && max1.z >= min2.z))
        {
            return true;
        }
        return false;
    }

    private bool SphereAABBCheck(GameObject a, GameObject b)
    {
        MeshFilter aMF = a.GetComponent<MeshFilter>();
        MeshFilter bMF = b.GetComponent<MeshFilter>();

        Bounds aB = aMF.mesh.bounds;
        Bounds bB = bMF.mesh.bounds;

        var min = Vector3.Scale(bB.min, b.transform.localScale) + b.transform.position;
        var max = Vector3.Scale(bB.max, b.transform.localScale) + b.transform.position;

        var x = Mathf.Max(min.x, Mathf.Min(a.transform.position.x, max.x));
        var y = Mathf.Max(min.y, Mathf.Min(a.transform.position.y, max.y));
        var z = Mathf.Max(min.z, Mathf.Min(a.transform.position.z, max.z));

        var distance = Mathf.Sqrt((x - a.transform.position.x) * (x - a.transform.position.x) +
            (y - a.transform.position.y) * (y - a.transform.position.y) +
            (z - a.transform.position.z) * (z - a.transform.position.z));

        return distance < aB.extents.magnitude;
    }
}
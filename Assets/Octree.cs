using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octree
{
    public OctreeNode rootNode;

    public Octree(GameObject[] gameObjects, float minSize)
    {
        Bounds bound = new Bounds();
        foreach(GameObject go in gameObjects) { bound.Encapsulate(go.GetComponent<Collider>().bounds); }
        float maxSize = Mathf.Max(new float[] { bound.size.x,bound.size.y,bound.size.z });
        Vector3 boundSize = 0.5f * new Vector3(maxSize, maxSize, maxSize);
        bound.SetMinMax(bound.center - boundSize, bound.center + boundSize);
        rootNode = new OctreeNode(bound, minSize);
        CheckObjects(gameObjects);
    }

    public void CheckObjects(GameObject[] gameObjects)
    {
        foreach (GameObject go in gameObjects) rootNode.CheckObject(go);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatOctree : MonoBehaviour
{
    public GameObject[] gameObjects;
    int minSize = 5;
    Octree octree;

    void Start()
    {
        octree = new Octree(gameObjects, minSize);
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying) octree.rootNode.Draw();
    }
}

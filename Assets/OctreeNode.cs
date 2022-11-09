using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctreeNode
{
    Bounds nodeBounds;
    Bounds []childBounds;
    OctreeNode[] child;
    float minSize;

    public OctreeNode(Bounds b,float f)
    {
        nodeBounds = b; minSize = f;
        Vector3 childSize = nodeBounds.size / 2;
        float eps = nodeBounds.size.x / 4;
        childBounds = new Bounds[8];
        childBounds[0] = new Bounds(nodeBounds.center + new Vector3(-eps, -eps, -eps), childSize);
        childBounds[1] = new Bounds(nodeBounds.center + new Vector3(-eps, -eps,  eps), childSize);
        childBounds[2] = new Bounds(nodeBounds.center + new Vector3(-eps,  eps, -eps), childSize);
        childBounds[3] = new Bounds(nodeBounds.center + new Vector3(-eps,  eps,  eps), childSize);
        childBounds[4] = new Bounds(nodeBounds.center + new Vector3( eps, -eps, -eps), childSize);
        childBounds[5] = new Bounds(nodeBounds.center + new Vector3( eps, -eps,  eps), childSize);
        childBounds[6] = new Bounds(nodeBounds.center + new Vector3( eps,  eps, -eps), childSize);
        childBounds[7] = new Bounds(nodeBounds.center + new Vector3( eps,  eps,  eps), childSize);
    }

    public void CheckObject(GameObject go)
    {
        if (nodeBounds.size.x < minSize) return;
        if (child == null) child = new OctreeNode[8];
        bool divide = false;
        for(int i=0;i<8;i++)
        {
            if (child[i] == null) child[i] = new OctreeNode(childBounds[i], minSize);
            if (child[i].nodeBounds.Intersects(go.GetComponent<Collider>().bounds)) { divide = true; child[i].CheckObject(go); }
        }
        if (divide == false) child = null;
    }

    public void Draw()
    {
        Gizmos.color = new Color(135f / 255, 206f / 255, 235f / 255);
        Gizmos.DrawWireCube(nodeBounds.center, nodeBounds.size);
        if (child != null) foreach (OctreeNode node in child) node.Draw();
    }
}

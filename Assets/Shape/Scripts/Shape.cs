using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape
{
    private Vector2[] nodePositions;
    
    public Shape(Vector2[] nodePositions)
    {
        this.nodePositions = nodePositions;
        CreateMesh();
    }
    
    private void CreateMesh()
    {
        Vector3 position = new Vector3(0, 0, 0);
        GameObject obj = new GameObject();
        MeshFilter meshFilter = obj.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer meshRenderer = obj.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
        meshRenderer.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1);

        Vector3[] verts = new Vector3[nodePositions.Length * 4];
        Vector2[] uvs = new Vector2[nodePositions.Length * 4];
        int[] tris = new int[nodePositions.Length * 2 * 3];
        int triCount = 0;
        int nodeCount = 0;

        for(int i = 0; i < verts.Length; i += 4)
        {
            //Set quad's vertices
            verts[i] = new Vector3(0 + nodePositions[nodeCount].x, 0 + nodePositions[nodeCount].y);
            verts[i + 1] = new Vector3(0 + nodePositions[nodeCount].x, 1 + nodePositions[nodeCount].y);
            verts[i + 2] =  new Vector3(1 + nodePositions[nodeCount].x, 1 + nodePositions[nodeCount].y);
            verts[i + 3] = new Vector3(1 + nodePositions[nodeCount].x, 0 + nodePositions[nodeCount].y);
            
            //Make new UVs to match new vertices
            uvs[i] = new Vector2(0 + nodePositions[nodeCount].x, 0 + nodePositions[nodeCount].y);
            uvs[i + 1] = new Vector2(0 + nodePositions[nodeCount].x, 1 + nodePositions[nodeCount].y);
            uvs[i + 2] =  new Vector2(1 + nodePositions[nodeCount].x, 1 + nodePositions[nodeCount].y);
            uvs[i + 3] = new Vector2(1 + nodePositions[nodeCount].x, 0 + nodePositions[nodeCount].y);
            
            //Create triangle 1 of new quad
            tris[triCount] = i;
            tris[triCount + 1] = i + 1;
            tris[triCount + 2] = i + 2;
            
            //Create triangle 2 onew quad
            tris[triCount + 3] = i;
            tris[triCount + 4] = i + 2;
            tris[triCount + 5] = i + 3;

            triCount += 6;
            nodeCount++;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = verts;
        mesh.uv = uvs;
        mesh.triangles = tris;
        mesh.name = "Piece";
        meshFilter.mesh = mesh;

    }
}

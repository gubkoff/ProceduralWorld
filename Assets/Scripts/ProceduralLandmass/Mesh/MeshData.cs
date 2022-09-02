using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HEXWorld.ProceduralLandmass
{
    public class MeshData
    {
        public Vector3[] vertices;
        private int[] triangles;
        public Vector2[] uvs;

        private const int NUMBER_EDGES_IN_SQUARE = 6;
        private const int NUMBER_OF_TRIANGLE_VERTICES = 3;

        private int triangleIndex;

        public MeshData(int width, int height)
        {
            vertices = new Vector3[width * height];
            uvs = new Vector2[width * height];
            triangles = new int[(width - 1) * (height - 1) * NUMBER_EDGES_IN_SQUARE];
        }

        public void AddTriangle(int a, int b, int c)
        {
            triangles[triangleIndex] = a;
            triangles[triangleIndex + 1] = b;
            triangles[triangleIndex + 2] = c;
            triangleIndex += NUMBER_OF_TRIANGLE_VERTICES;
        }

        public Mesh CreateMesh()
        {
            Mesh mesh = new Mesh
            {
                name = "Procedural mesh"
            };
            
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            mesh.RecalculateNormals();
            return mesh;
        }
    }
}
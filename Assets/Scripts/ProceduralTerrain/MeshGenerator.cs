using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HEXWorld.ProceduralTerrain
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class MeshGenerator : MonoBehaviour
    {
        private Mesh mesh;
        private Vector3[] vertices;
        private int[] triangles;

        private const int VERT_COUNT_IN_SQUAD = 6;

        [SerializeField] private int xSize = 20;
        [SerializeField] private int zSize = 20;

        private void Start()
        {
            mesh = new Mesh()
            {
                name = "Procedural terrain"
            };
            GetComponent<MeshFilter>().mesh = mesh;

            CreateShape();
            UpdateMesh();
        }

        private void CreateShape()
        {
            //Create vertices
            vertices = new Vector3[(xSize + 1) * (zSize + 1)];

            for (int i = 0, z = 0; z <= zSize; z++)
            {
                for (int x = 0; x <= xSize; x++)
                {
                    float y = Mathf.PerlinNoise(x * 0.3f, z * 0.3f) * 3;
                    vertices[i] = new Vector3(x, y, z);
                    i++;
                }
            }
            
            //Create triangles
            triangles = new int[xSize * zSize * VERT_COUNT_IN_SQUAD];
            int vert = 0;
            int tris = 0;

            for (int z = 0; z < zSize; z++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    triangles[tris + 0] = vert + 0;
                    triangles[tris + 1] = vert + xSize + 1;
                    triangles[tris + 2] = vert + 1;
                    triangles[tris + 3] = vert + 1;
                    triangles[tris + 4] = vert + xSize + 1;
                    triangles[tris + 5] = vert + xSize + 2;

                    vert++;
                    tris += VERT_COUNT_IN_SQUAD;
                }

                vert++;
            }

            for (int i = 0; i < triangles.Length; i++)
            {
                Debug.Log($"TRIANGLE: {i} : {triangles[i]}");
            }
        }

        private void UpdateMesh()
        {
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
        }

        private void OnDrawGizmos()
        {
            if (vertices == null)
                return;

            for (int i = 0; i < vertices.Length; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(vertices[i], 0.1f);
            }
        }
    }
}
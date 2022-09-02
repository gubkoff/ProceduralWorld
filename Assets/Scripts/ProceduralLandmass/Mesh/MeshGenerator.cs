using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HEXWorld.ProceduralLandmass {
    public static class MeshGenerator {
        public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier,
            AnimationCurve heightCurve, int levelOfDetail) {
            var width = heightMap.GetLength(0);
            var height = heightMap.GetLength(1);
            float topLeftX = (width - 1) / -2f;
            float topLeftZ = (height - 1) / 2f;

            int meshSimplificationIncrement = levelOfDetail == 0 ? 1 : levelOfDetail * 2;
            int verticesPerLine = (width - 1) / meshSimplificationIncrement + 1;

            MeshData meshData = new MeshData(verticesPerLine, verticesPerLine);

            for (int vertexIndex = 0, y = 0; y < height; y += meshSimplificationIncrement) {
                for (int x = 0; x < width; x += meshSimplificationIncrement) {
                    meshData.vertices[vertexIndex] = new Vector3(topLeftX - x,
                        heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier, topLeftZ + y);
                    meshData.uvs[vertexIndex] = new Vector2(x / (float) width, y / (float) height);

                    if (x < width - 1 && y < height - 1) {
                        meshData.AddTriangle(vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
                        meshData.AddTriangle(vertexIndex + verticesPerLine + 1, vertexIndex, vertexIndex + 1);
                    }

                    vertexIndex++;
                }
            }

            return meshData;
        }
    }
}
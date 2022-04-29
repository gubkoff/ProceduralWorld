using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HEXWorld.ProceduralLandmass
{
    public class MapDisplay : MonoBehaviour
    {
        [SerializeField] private Renderer textureRenderer;

        public void DisplayMap(float[,] noiseMap, TerrainType[] terrainTypes, DrawMode drawMode = DrawMode.NoiseMap)
        {
            DrawTexture(GetTextureByDrawMode(noiseMap, terrainTypes, drawMode));
        }

        private void DrawTexture(Texture2D texture)
        {
            textureRenderer.sharedMaterial.mainTexture = texture;
            textureRenderer.transform.localScale = new Vector3(texture.width, 1f, texture.height);
        }

        private Texture2D GetTextureByDrawMode(float[,] noiseMap, TerrainType[] regions, DrawMode drawMode)
        {
            int width = noiseMap.GetLength(0);
            int height = noiseMap.GetLength(1);

            var texture = new Texture2D(width, height);

            switch (drawMode)
            {
                case DrawMode.NoiseMap:
                    texture = TextureGenerator.TextureFromHeightMap(noiseMap);
                    break;
                case DrawMode.ColorMap:
                    texture = TextureGenerator.TextureFromColorMap(GetColorMapFromHeightMap(width, height, regions,
                        noiseMap), width, height);
                    break;
                default:
                    Debug.LogError("Not set draw mode");
                    break;
            }

            return texture;
        }

        private Color[] GetColorMapFromHeightMap(int width, int height, TerrainType[] regions, float[,] heightMap)
        {
            Color[] colorMap = new Color[width * height];
            for (int i = 0, y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int r = 0; r < regions.Length; r++)
                    {
                        if (heightMap[x, y] <= regions[r].height)
                        {
                            colorMap[i] = regions[r].color;
                            break;
                        }
                    }
                    i++;
                }
            }

            return colorMap;
        }
    }
}
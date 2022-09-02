using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HEXWorld.ProceduralLandmass
{
    public static class TextureGenerator
    {
        public static Texture2D TextureFromColorMap(Color[] colorMap, int width, int height)
        {
            Texture2D texture = new Texture2D(width, height);
            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.SetPixels(colorMap);
            texture.Apply();
            return texture;
        }

        public static Texture2D TextureFromHeightMap(float[,] heightMap)
        {
            int width = heightMap.GetLength(0);
            int height = heightMap.GetLength(1);

            Color[] colorMap = new Color[width * height];

            for (int i = 0, y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    colorMap[i] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
                    i++;
                }
            }

            return TextureFromColorMap(colorMap, width, height);
        }
    }
}

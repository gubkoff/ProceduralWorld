using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HEXWorld.ProceduralLandmass
{
    public class MapDisplay : MonoBehaviour
    {
        [SerializeField] private Renderer textureRenderer;

        public void DrawNoiseMap(float[,] noiseMap)
        {
            int width = noiseMap.GetLength(0);
            int height = noiseMap.GetLength(1);

            Texture2D texture = new Texture2D(width, height);

            Color[] colorMap = new Color[width * height];

            for (int i = 0, y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    colorMap[i] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
                    i++;
                }
            }
            texture.SetPixels(colorMap);
            texture.Apply();

            //TODO разобраться с работой текстур
            textureRenderer.sharedMaterial.mainTexture = texture;
            textureRenderer.transform.localScale = new Vector3(width, 1f, height);
        }
    }
}
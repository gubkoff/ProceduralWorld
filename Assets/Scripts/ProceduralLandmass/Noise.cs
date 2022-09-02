using Unity.Mathematics;
using UnityEngine;

namespace HEXWorld.ProceduralLandmass
{
    public static class Noise
    {
        public static float[,] GenerateHeightMap(int mapWidth, int mapHeight, int seed, float scale, int octaves,
            float persistence, float lacunarity, Vector2 offset)
        {
            float[,] noiseMap = new float[mapWidth, mapHeight];
            Vector2[] octaveOffset = GetOctaveOffset(seed, octaves, offset);
            Vector2 center = GetCenter(mapWidth, mapHeight);

            if (scale <= 0)
            {
                scale = 0.0001f;
            }

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    float amplitude = 1f;
                    float frequency = 1f;
                    float noiseHeight = 0f;

                    for (int i = 0; i < octaves; i++)
                    {
                        float sampleX = (x - center.x) / scale * frequency + octaveOffset[i].x;
                        float sampleY = (y - center.y) / scale * frequency + octaveOffset[i].y;

                        float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                        noiseHeight += perlinValue * amplitude;

                        amplitude *= persistence;
                        frequency *= lacunarity;
                    }

                    noiseMap[x, y] = noiseHeight;
                }
            }

            return NormalizeNoiseMap(noiseMap);
        }

        private static float[,] NormalizeNoiseMap(float[,] noiseMap)
        {
            float maxNoiseHeight = float.MinValue;
            float minNoiseHeight = float.MaxValue;
            int width = noiseMap.GetLength(0);
            int height = noiseMap.GetLength(1);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (noiseMap[x, y] > maxNoiseHeight)
                    {
                        maxNoiseHeight = noiseMap[x, y];
                    }
                    else if (noiseMap[x, y] < minNoiseHeight)
                    {
                        minNoiseHeight = noiseMap[x, y];
                    }
                }
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
                }
            }

            return noiseMap;
        }

        private static Vector2[] GetOctaveOffset(int seed, int octaves, Vector2 offset)
        {
            System.Random prng = new System.Random(seed);
            Vector2[] octaveOffset = new Vector2[octaves];

            for (int i = 0; i < octaves; i++)
            {
                var offsetX = prng.Next(-100000, 100000) + offset.x;
                var offsetY = prng.Next(-100000, 100000) + offset.y;
                octaveOffset[i] = new Vector2(offsetX, offsetY);
            }

            return octaveOffset;
        }

        private static Vector2 GetCenter(int width, int height)
        {
            return new Vector2(width / 2f, height / 2f);
        }
    }
}
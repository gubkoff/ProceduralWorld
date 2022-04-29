using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HEXWorld.ProceduralLandmass
{
    public class MapGenerator : MonoBehaviour
    {
        public bool isAutoUpdate;
        public DrawMode drawMode;

        [SerializeField] private int mapWidth;
        [SerializeField] private int mapHeight;
        [SerializeField] private float noiseScale;
        [SerializeField] private int seed;
        [SerializeField] private int octaves;
        [Range(0, 1)] [SerializeField] private float persistence;
        [SerializeField] private float lacunarity;
        [SerializeField] private Vector2 offset;
        [SerializeField] private TerrainType[] regions;
        

        private MapDisplay _mapDisplay;

        private void Start()
        {
            GenerateMap();
        }

        public void GenerateMap()
        {
            var noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistence,
                lacunarity, offset);
            GetComponent<MapDisplay>().DisplayMap(noiseMap, regions, drawMode);
        }
        
        private void OnValidate()
        {
            if (mapWidth < 1)
            {
                mapWidth = 1;
            }
            if (mapHeight < 1)
            {
                mapHeight = 1;
            }
            if (lacunarity < 1)
            {
                lacunarity = 1;
            }
            if (octaves < 0)
            {
                octaves = 0;
            }
        }
    }
}
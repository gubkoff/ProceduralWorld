using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace HEXWorld.ProceduralLandmass {
    public class MapGenerator : MonoBehaviour {
        public bool isAutoUpdate;
        public DrawMode drawMode;

        private const int mapChunkSize = 241;
        [Range(0, 6)] [SerializeField] private int levelOfDetail;
        [SerializeField] private float noiseScale;

        [SerializeField] private int seed;
        [SerializeField] private int octaves;
        [Range(0, 1)] [SerializeField] private float persistence;
        [SerializeField] private float lacunarity;
        [SerializeField] private Vector2 offset;

        [SerializeField] private float meshHeightMultiplier;
        [SerializeField] private AnimationCurve meshHeightCurve;

        [SerializeField] private TerrainType[] regions;

        private void Start() {
            GenerateMap();
        }

        public void GenerateMap() {
            var heightMap = Noise.GenerateHeightMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistence,
                lacunarity, offset);
            var meshData =
                MeshGenerator.GenerateTerrainMesh(heightMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail);
            GetComponent<MapDisplay>().DisplayMap(heightMap, regions, drawMode, meshData);
        }

        private void OnValidate() {
            if (lacunarity < 1) {
                lacunarity = 1;
            }

            if (octaves < 0) {
                octaves = 0;
            }
        }
    }
}
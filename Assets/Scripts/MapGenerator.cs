using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// generates the map
public class MapGenerator : MonoBehaviour
{
    public enum DrawMode {NoiseMap, ColorMap, Mesh};

    const int mapChunkSize = 241;

    [Range(0, 6)]
    public int levelOfDetail;

    public DrawMode drawMode;
    public float scale;
    public int octaves;

    [Range(0, 1)]
    public float persistence;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public float meshHeightMultiplier;

    public AnimationCurve meshHeightCurve;

    public TerrainType[] regions;



    public void GenerateMap() {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, scale, octaves, persistence, lacunarity, offset);

        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];
        for(int y = 0; y < mapChunkSize; y++) {
            for(int x = 0; x < mapChunkSize; x++) {
                float currentHeight = noiseMap[x, y];
                for(int i = 0; i < regions.Length; i++) {
                    if (currentHeight <= regions[i].height) {
                        colorMap[y * mapChunkSize + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap) {
            display.DrawTexture(TextureGenerator.textureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColorMap) {
            display.DrawTexture(TextureGenerator.textureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.Mesh) {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail), TextureGenerator.textureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
        }
    }

    void OnValidate() {
        if (lacunarity < 1) {
            lacunarity = 1;
        }
        if (octaves < 0) {
            octaves = 0;
        }
    }
}

[System.Serializable]
public struct TerrainType {
    public string name;
    public float height;
    public Color color;


}

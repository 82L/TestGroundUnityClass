using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public enum TileType
{
    Grass,
    Forest,
    Water,
    Mountain
}


public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int m_terrainSizeX;
    [SerializeField] private int m_terrainSizeY;
    [SerializeField] private GameObject m_prefabForest;
    [SerializeField] private GameObject m_prefabMountain;
    [SerializeField] private GameObject m_prefabWater;
    [SerializeField] private GameObject m_prefabGrass;
    [SerializeField]private int seed = 1;
    private TileType[,] m_terrain;
    private List<GameObject> m_spawnedObject = new ();

    private void Start()
    {
        Random.InitState(seed);
        GenerateTerrain();
    }

    public void BtnGenerateTerrain()
    {
        GenerateTerrain();
    }

    private void GenerateTerrain()
    {
        m_terrain = new TileType[m_terrainSizeX, m_terrainSizeY];
        Generate(TileType.Mountain, new float2(20f,40f), NoiseType.Perlin, 0.8f);
        Generate(TileType.Forest, new float2(20f,40f), NoiseType.Simplex, 0.8f);
        Generate(TileType.Water, new float2(50f,70f), NoiseType.Cellular, 0.8f);
        Spawn();
    }


    private void Generate(TileType p_tileType, float2 p_scaleMinMax, NoiseType p_noiseType, float p_greaterThanValue)
    {
        float2 l_noiseOffset = new float2(Random.Range(0f, 100000f), Random.Range(0f, 100000f));
        float l_noiseScale = Random.Range(p_scaleMinMax.x, p_scaleMinMax.y);
        for (int l_x = 0; l_x < m_terrainSizeX; l_x++)
        {
            for (int l_y = 0; l_y < m_terrainSizeX; l_y++)
            {
                float2 l_coord;
                l_coord.x = (l_noiseOffset.x + l_x) / l_noiseScale;
                l_coord.y = (l_noiseOffset.y + l_y) / l_noiseScale;
                float sample = GetNoiseValue(l_coord, p_noiseType);
                if (sample > p_greaterThanValue)
                {
                    m_terrain[l_x, l_y] = p_tileType;
                }
            }
        }
       
       
    }
    
    private float GetNoiseValue(float2 p_position, NoiseType p_noiseType)
    {
        float p_noiseResult = 0f;
        switch (p_noiseType)
        {
            case NoiseType.Cellular:
                p_noiseResult = noise.cellular(p_position).x;
                break;
            case NoiseType.Perlin:
                p_noiseResult = noise.cnoise(p_position);
                break;
            case NoiseType.Simplex:
                p_noiseResult = noise.snoise(p_position);
                break;
            default:
                p_noiseResult = noise.cellular(p_position).y;
                break;
            
        }

        // noiseResult = (noiseResult + 1f) / 2f;
        p_noiseResult = math.remap(-1f, 1f, 0f, 1f, p_noiseResult);
        // p_noiseResult = math.pow(p_noiseResult, resultPow);
        return p_noiseResult;
    }
    
    private void Spawn()
    {
        foreach (var l_go in m_spawnedObject)
        {
            Destroy(l_go);
        }
        for (int l_x = 0; l_x < m_terrainSizeX; l_x++)
        {
            for (int l_y = 0; l_y < m_terrainSizeX; l_y++)
            {
                var prefab = GetPrefab(m_terrain[l_x, l_y]);
                m_spawnedObject.Add(SpawnTile(prefab, l_x, l_y));
            }
        }
    }

    private GameObject GetPrefab(TileType p_tileType)
    {
        return p_tileType switch
        {
            TileType.Mountain => m_prefabMountain,
            TileType.Forest => m_prefabForest,
            TileType.Water => m_prefabWater,
            TileType.Grass => m_prefabGrass,
            _ => m_prefabGrass
        };
    }
    private GameObject SpawnTile(GameObject p_gameObject, int p_x, int p_y)
    {
        GameObject l_go = Instantiate(p_gameObject, transform);
        l_go.transform.localPosition = new Vector3(p_x, 0f, p_y);
        return l_go;
    }
    
}

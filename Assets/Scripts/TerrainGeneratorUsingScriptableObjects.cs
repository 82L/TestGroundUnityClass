using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerrainGeneratorUsingScriptableObjects : MonoBehaviour
{
    [SerializeField]private int seed = 1;
    [SerializeField] private int m_terrainSizeX;
    [SerializeField] private int m_terrainSizeY;
    [SerializeField] private List<TerrainBiomeData> m_terrainBiomeDatas;
        
    private TerrainBiomeData[,] m_terrainBiomeData;
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
       
        m_terrainBiomeData = new TerrainBiomeData[m_terrainSizeX, m_terrainSizeY];
        foreach (var l_terrainBiomeData in m_terrainBiomeDatas)
        {
            l_terrainBiomeData.Generate(ref m_terrainBiomeData);
        }
        Spawn();
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
                var prefab =  m_terrainBiomeData[l_x,l_y].GetPrefab();
                m_spawnedObject.Add(SpawnTile(prefab, l_x, l_y));
            }
        }
    }

        
    private GameObject SpawnTile(GameObject p_gameObject, int p_x, int p_y)
    {
        GameObject l_go = Instantiate(p_gameObject, transform);
        l_go.transform.localPosition = new Vector3(p_x, 0f, p_y);
        return l_go;
    }
}
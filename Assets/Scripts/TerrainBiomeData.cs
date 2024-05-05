using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Terrain Biome Data", menuName = "Terrain Biome Data", order = 0)]
    public class TerrainBiomeData : ScriptableObject
    {
        [FormerlySerializedAs("prefabs")] [SerializeField]
        private List<GameObject> m_prefabs = new();
        [SerializeField]
        private List<TerrainBiomeNoiseData> m_noises = new();

        [Range(0, 1f)] [SerializeField] private float m_ifGreaterThan;

        [SerializeField] private AnimationCurve m_curveX;
        [SerializeField] private AnimationCurve m_curveZ;
        

        public void Generate(ref TerrainBiomeData[,] p_terrain)
        {
            float[,] l_noiseValues = new float[p_terrain.GetLength(0), p_terrain.GetLength(1)];
            foreach (var l_noise in m_noises)
            {
                float2 l_noiseOffset = new float2(Random.Range(l_noise.noiseOffsetMinMax.x, l_noise.noiseOffsetMinMax.y),
                    Random.Range(l_noise.noiseOffsetMinMax.x, l_noise.noiseOffsetMinMax.y));
                float l_noiseScale = Random.Range(l_noise.noiseScaleMinMax.x, l_noise.noiseScaleMinMax.y);
                for (int l_x = 0; l_x < l_noiseValues.GetLength(0); l_x++)
                {
                    for (int l_y = 0; l_y < l_noiseValues.GetLength(1); l_y++)
                    {
                        float2 l_coord;
                        l_coord.x = (l_noiseOffset.x + l_x) / l_noiseScale;
                        l_coord.y = (l_noiseOffset.y + l_y) / l_noiseScale;
                        float sample = l_noise.GetNoiseValue(l_coord) / m_noises.Count;
                        sample *= m_curveX.Evaluate(l_x / (float)l_noiseValues.GetLength(0));
                        sample *= m_curveZ.Evaluate(l_y / (float)l_noiseValues.GetLength(1));
                        l_noiseValues[l_x, l_y] += sample;

                    }
                }
            }

            for (int l_x = 0; l_x < l_noiseValues.GetLength(0); l_x++)
            {
                for (int l_y = 0; l_y < l_noiseValues.GetLength(1); l_y++)
                {
                    if (l_noiseValues[l_x, l_y] > m_ifGreaterThan)
                    {
                        p_terrain[l_x, l_y] = this;
                    }
                }
            }
        }
       
        
        public GameObject GetPrefab()
        {
            int rnd = Random.Range(0, m_prefabs.Count);
            return m_prefabs[rnd];
        }
    }
}
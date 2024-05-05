using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public enum NoiseType
    {
        Cellular,
        Perlin,
        Simplex,
        Full
    }
    [Serializable]
    public class TerrainBiomeNoiseData 
    {
  
        public NoiseType noiseType;
        public float2 noiseScaleMinMax;
        public float2 noiseOffsetMinMax;
        
        public float GetNoiseValue(float2 p_position)
        {
            float l_noiseResult = 0f;
            switch (noiseType)
            {
                case NoiseType.Full:
                    return 1f;
                case NoiseType.Cellular:
                    l_noiseResult = noise.cellular(p_position).x;
                    break;
                case NoiseType.Perlin:
                    l_noiseResult = noise.cnoise(p_position);
                    break;
                case NoiseType.Simplex:
                    l_noiseResult = noise.snoise(p_position);
                    break;
                default:
                    l_noiseResult = noise.cellular(p_position).y;
                    break;
            
            }

            // noiseResult = (noiseResult + 1f) / 2f;
            l_noiseResult = math.remap(-1f, 1f, 0f, 1f, l_noiseResult);
            // p_noiseResult = math.pow(p_noiseResult, resultPow);
            return l_noiseResult;
        }
    }
}
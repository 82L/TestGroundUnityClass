using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TextureNoise : MonoBehaviour
{
    [SerializeField] 
    private Renderer targetRenderer;

    [SerializeField] 
    private int textureSizeX = 1024;
    [SerializeField] 
    private int textureSizeY = 1024;

    public enum NOISE_TYPE
    {
        Cellular,
        Perlin,
        Simplex
    }
    
    [SerializeField]
    private NOISE_TYPE noiseType;
    
    [SerializeField] 
    private float2 noiseOffset;

    [SerializeField] 
    private float noiseScale = 100f;
    [SerializeField]
    private float resultPow = 1f;
    
    
    private Texture2D _tex;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        _tex = new Texture2D(textureSizeX, textureSizeY);
        targetRenderer.material.mainTexture = _tex;
       
        return null;
    }

    [ContextMenu("TextureGenerator")]
    public void TextureGenerator()
    {
        for (int x = 0; x< textureSizeX; x++)
        {
            for (int y = 0; y < textureSizeY; y++)
            {
               
                float2 coord;
                coord.x = (noiseOffset.x + x) / textureSizeX * noiseScale;
                coord.y = (noiseOffset.y + y) / textureSizeY * noiseScale;
                float sample = GetNoiseResult(coord);
                Color color = new Color(sample,sample,sample);
                _tex.SetPixel(x, y, color);
            }
        }
        _tex.Apply();

    }

    private float GetNoiseResult(float2 position)
    {
        float noiseResult = 0f;
        switch (noiseType)
        {
            case NOISE_TYPE.Cellular:
                noiseResult = noise.cellular(position).x;
                break;
            case NOISE_TYPE.Perlin:
                noiseResult = noise.cnoise(position);
                break;
            case NOISE_TYPE.Simplex:
                noiseResult = noise.snoise(position);
                break;
            default:
                noiseResult = noise.cellular(position).y;
                break;
            
        }

        // noiseResult = (noiseResult + 1f) / 2f;
        noiseResult = math.remap(-1f, 1f, 0f, 1f, noiseResult);
        noiseResult = math.pow(noiseResult, resultPow);
        return noiseResult;
    }

    
}


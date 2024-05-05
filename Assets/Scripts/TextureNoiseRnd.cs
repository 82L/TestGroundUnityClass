using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TextureNoiseRnd : MonoBehaviour
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
    private int seed;
    // [SerializeField] 
    // private float2 noiseOffset;
    //
    // [SerializeField] 
    // private float noiseScale = 100f;
    // [SerializeField]
    // private float resultPow = 1f;
    //
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
        Random.InitState(seed);
       

        StartCoroutine(GenerationRoutine());

    }

    private IEnumerator GenerationRoutine()
    {
        float2 noiseOffset = new float2( Random.Range(-100000f, 100000f), Random.Range(-100000f, 100000f));
       
        float noiseScale = Random.Range(5f, 300f);
        float resultPow = Random.Range(0.1f, 5f);
        int iteration = 0;
        int stepsIteration = Mathf.RoundToInt((textureSizeX * textureSizeY) / 100f);
        for (int x = 0; x< textureSizeX; x++)
        {
            for (int y = 0; y < textureSizeY; y++)
            {
                iteration++;
                float2 coord;
                coord.x = (noiseOffset.x + x) / textureSizeX * noiseScale;
                coord.y = (noiseOffset.y + y) / textureSizeY * noiseScale;
                float sample = GetNoiseResult(coord, resultPow);
                Color color = new Color(sample,sample,sample);
                _tex.SetPixel(x, y, color);
                if (iteration % stepsIteration == 0)
                {
                    _tex.Apply();
                    yield return new WaitForSeconds(0.05f);
               
                }
            }
        }
        _tex.Apply();
        
    }

    private float GetNoiseResult(float2 position, float resultPow)
    {
        float noiseResult;
       
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


using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class TowerGenerator : MonoBehaviour
{
    private const int SIZE = 101;
    [SerializeField] private GameObject m_prefabTower;
    [SerializeField] private int m_nbRandomToGenerate;

    [SerializeField] private int m_randomMax;
    private List<Tuple<int, GameObject>> m_graph = new ();
   
    
    private int m_nbRandom = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        for (int l_i = 0; l_i < m_randomMax; l_i++)
        {
            var l_graphItem = Instantiate(m_prefabTower, this.transform);
            l_graphItem.transform.localPosition = new Vector3(l_i, 0f, 0f);
            m_graph.Add(new Tuple<int, GameObject>(0, l_graphItem));
        }

        StartCoroutine(AddRandom(10));
    }

    private IEnumerator AddRandom(int p_nb)
    {
        m_nbRandom += p_nb;
        for (int i = 0; i < p_nb; i++)
        {
            int random = Random.Range(0, m_randomMax);
            m_graph[random] = new Tuple<int, GameObject>( m_graph[random].Item1+1, m_graph[random].Item2);
        }
        RefreshGraph();
        
        if (m_nbRandom >= m_nbRandomToGenerate)
        {
            yield break;
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(AddRandom(10));
    }

    private void RefreshGraph()
    {
        foreach (var t in m_graph)
        {
            float scale = t.Item1;
            t.Item2.transform.localScale = new Vector3(1f, scale, 1f);
        }
    }
    
   
}

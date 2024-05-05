using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class Manager : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.1f;
    [FormerlySerializedAs("movableObjects")] [SerializeField]
    private List<Character> characters = new ();

    private int count;
    public int Count
    {
        get { return count; }
        private set
        {
            if (value < 0)
            {
                Debug.Log("Trop bas");
            }

            count = value;
            OnCountChanged?.Invoke();
        }
    }
    
    /**
     * mot clé event permet d'avoir une liste d'events
     */

    public event Action OnCountChanged;
    // Start is called before the first frame update
    void Start()
    {
        OnCountChanged += CountChanged;

    }

    private void CountChanged()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (var character in characters)
        {
            float deltaTime = Time.deltaTime;
            var position = character.transform.position;
            float posX = position.x + (character.Speed * deltaTime);
            position = new Vector3(posX,position.y, position.z);
            character.transform.position = position;
        }
    }

    private void OnDestroy()
    {
        OnCountChanged -= CountChanged;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlane : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    
    public enum TEST_ENUM
    {
        NONE,
        ENUM_1,
        ENUM_2
    }

    [SerializeField]private TEST_ENUM noise;
    private void Awake(){
        Debug.Log("Awake");
    }   
    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Start");

    }

    // Update is called once per frame
    private void Update()
    {
        // Debug.Log("Update");
        float deltaTime = Time.deltaTime;
        float posX = transform.position.x + (speed * deltaTime);
        transform.position = new Vector3(posX,0, 0);
    }

    private void OnEnable()
    {
        Debug.Log("Enabled");
    }

    private void OnDisable()
    {
        Debug.Log("Disable");
    }

    private void FixedUpdate()
    {
    }

    private void OnDestroy()
    {
        Debug.Log("Destroy");

    }
}

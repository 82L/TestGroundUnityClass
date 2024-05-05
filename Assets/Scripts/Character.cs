using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.1f;
    // public float Speed { get; }
    public float Speed => speed;
}

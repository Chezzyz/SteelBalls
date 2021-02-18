using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RoadManager : MonoBehaviour
{
    [Range(0,100)] public float roadSpeed;

    void Start()
    {
        
    }

    public void Move()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.back * roadSpeed * Time.deltaTime);
    }
}

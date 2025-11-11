using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLerp : MonoBehaviour
{
    public Transform starttransform;

    public Transform targettansform;

    private Vector2 startPos;
    private Vector2 targetPos;
    private float time = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = starttransform.position;
        targetPos = targettansform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        time += Time.deltaTime;
    }
}

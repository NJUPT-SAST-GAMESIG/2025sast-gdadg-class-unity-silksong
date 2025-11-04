using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Lerp : MonoBehaviour
{
    public Transform targetTransform;
    public Vector3 target;
    public Vector3 start;

    private float _totalTime = 0;
    void Start()
    {
        start = transform.position;
        target = targetTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(start, target, _totalTime);
        
        _totalTime += Time.deltaTime * 0.2f;
    }
}

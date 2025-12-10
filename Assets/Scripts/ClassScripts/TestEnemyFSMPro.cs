using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyFSMPro : MonoBehaviour
{
    private TestFSM fsm;
    
    
    // Start is called before the first frame update
    void Start()
    {
        fsm = new TestFSM();
    }

    // Update is called once per frame
    void Update()
    {
        fsm.OnUpdateState();
    }
    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Attack : MonoBehaviour
{
    [SerializeField] private MeleeWeapon _meleeWeapon;
    
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            _meleeWeapon.ExecuteAttack();
        }
            
    }
}

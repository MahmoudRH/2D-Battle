using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight1Script : HerosScript
{

    Knight1Script()
    {
        heroHealth = 10;
        attackPower = 5;
        moveSpeed = 1.4f;  
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();   
    }
}

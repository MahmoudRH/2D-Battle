using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight3Script : HerosScript
{

    Knight3Script()
    {
        heroHealth = 30;
        attackPower = 15;
        moveSpeed = 1.2f;
        // printHeroInfo();
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

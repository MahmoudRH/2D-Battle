using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight2Script : HerosScript
{

    Knight2Script()
    {
         heroHealth = 15;
         attackPower = 10;
         moveSpeed = 1.6f;
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

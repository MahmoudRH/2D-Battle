using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ORCsScript : MonoBehaviour
{
    static int numOfAliveORCs = 0;
    //ORC states:
    private int idle = 0, moving = 1, attacking = 2, dead = 3;
    //ORC properities: 
    public int orcHealth, attackPower, orcState;
    public float moveSpeed;
    //target type:
    public int target;
    public int Hero_Target = 0, Castle_Target = 1;

    [SerializeField]
    Transform herosCastle;

    [SerializeField]
    Animator orcAnimator;


    private void Awake()
    {
        numOfAliveORCs++;
        orcState = moving;
    }


    public void Update()
    {
        orcAnimator.SetInteger("orcState", orcState);

        if (HerosScript.numOfAliveHeros > 0)
        {
            //move to their location
        }
        else
        {
            //move to heros castle
            if (orcState == moving)
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }
            else if (orcState == attacking)
            {

                if (target == Castle_Target)
                {
                    if (HerosCastleScript.castleHealth > 0)
                        HerosCastleScript.castleHealth -= attackPower * Time.deltaTime;
                    else
                        orcState = idle;
                }
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "HerosCastle")
        {
            target = Castle_Target;
            orcState = attacking;
        }
    }
}

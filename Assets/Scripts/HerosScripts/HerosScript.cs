using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerosScript : MonoBehaviour
{
    private int moving = 1, attacking = 2, dead = 3;
    static int numOfAliveHeros =0 ;
    public int heroHealth, attackPower, heroState;
    public float moveSpeed;
    public int target;
    public int ORC_Target = 0, Castle_Target = 1;
    [SerializeField]
    Transform enemyCastle;

    [SerializeField]
    Animator heroAnimator;
    //temp
    int numOfAliveEnemies = 0;

    private void Awake()
    {
        numOfAliveHeros++;
        heroState = moving;
    }

    public void Update()
    {
        heroAnimator.SetInteger("heroState", heroState);

        if (numOfAliveEnemies > 0)
        {
            //move to their location
        }
        else
        {
            //move to enemy castle
            if(heroState == moving)
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);    
            }
            else if(heroState == attacking){

                if (target == Castle_Target)
                {
                    EnemyCastleScript.castleHealth -= attackPower * Time.deltaTime;
                }
            }
            
        }
        
    }

    public void printHeroInfo()
    {
        Debug.Log("HeroHealth= " + heroHealth+", HeroPower= "+ attackPower+", HeroSpeed= "+moveSpeed);
        Debug.Log("Hero Created, Num of Alive Heros = " + numOfAliveHeros);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "EnemyCastle")
        {
            target = Castle_Target;
            heroState = attacking;     
        }
    }


}

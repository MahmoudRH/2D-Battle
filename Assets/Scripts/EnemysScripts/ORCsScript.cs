using System.Collections.Generic;
using UnityEngine;

public class ORCsScript : MonoBehaviour
{
    public static int numOfAliveORCs = 0;
    public static List<GameObject> AliveORCsList = new List<GameObject>();

    //ORC properities: 
    public float attackPower, orcHealth, moveSpeed;

    TargetType Target;
    CharacterState OrcState;

    [SerializeField]
    Transform herosCastle;
    [SerializeField]
    Animator orcAnimator;

    private void Awake()
    {
        numOfAliveORCs++;
        AliveORCsList.Add(this.gameObject);
        OrcState = CharacterState.MOVING;
        herosCastle = GameObject.Find("HeroCastle").transform;
    }


    public void Update()
    {
        //-> Check if ORC is still alive
        if (orcHealth <= 0)
        {
            OrcState = CharacterState.DEAD;
        }

        //-> Track ORC State
        switch (OrcState)
        {
            case CharacterState.IDLE:
                OrcIsIdle();
                break;


            case CharacterState.MOVING:
                OrcIsMoving();
                break;

            case CharacterState.ATTACKING:
                OrcIsAttacking();
                break;

            case CharacterState.DEAD:
                OrcIsDead();
                break;
        }

        //-> Update ORC animation
        orcAnimator.SetInteger("orcState", (int)OrcState);
    }

    private void OrcIsDead()
    {
        AliveORCsList.Remove(this.gameObject);
        Destroy(this.gameObject, 0.30f);
        numOfAliveORCs--;
    }

    private void OrcIsAttacking()
    {
        switch (Target)
        {
            case TargetType.CastleTarget:
                if (HerosCastleScript.castleHealth > 0)
                    HerosCastleScript.castleHealth -= attackPower * Time.deltaTime;
                else
                    OrcState = CharacterState.IDLE;
                break;

            case TargetType.HeroTarget:
                //Attack The Hero
                break;
        }
    }

    private void OrcIsMoving()
    {
        if (HerosScript.numOfAliveHeros > 0)
        {
            //move to their location
        }
        else
        {
            //move to heros castle
            if (herosCastle != null)
                transform.position = Vector2.MoveTowards(transform.position, herosCastle.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OrcIsIdle()
    {
        //TODO idle
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "HeroCastle")
        {
            Target = TargetType.CastleTarget;
            OrcState = CharacterState.ATTACKING;
        }
    }

}

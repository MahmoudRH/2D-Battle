using System.Collections.Generic;
using UnityEngine;


public class HerosScript : MonoBehaviour
{
    public static int numOfAliveHeros = 0;
    public static List<GameObject> AliveHerosList = new List<GameObject>();

    //hero properities: 
    public float moveSpeed, heroHealth, attackPower;

    public CharacterState HeroState;
    public TargetType Target;

    [SerializeField]
    Transform enemyCastle;
    Vector2 enemyCastlePosition;

    [SerializeField]
    Animator heroAnimator;

    GameObject orcTarget;
    ORCsScript targetScript;

    private bool reachedTheCastle = false;
    private void Awake()
    {
        numOfAliveHeros++;
        AliveHerosList.Add(this.gameObject);
        HeroState = CharacterState.MOVING;
        enemyCastle = GameObject.Find("EnemyCastle").transform;
        enemyCastlePosition = new Vector2(enemyCastle.position.x-2,enemyCastle.position.y);
        //Debug.Log("EnemyCastle Location: "+enemyCastle.position);
    }

    public void Update()
    {

        //-> Check if Hero is still alive
        if (heroHealth <= 0)
        {
            HeroState = CharacterState.DEAD;
        }


        //-> Track Hero State
        switch (HeroState)
        {
            case CharacterState.IDLE:
                HeroIsIdle();
                break;


            case CharacterState.MOVING:
                HeroIsMoving();
                break;

            case CharacterState.ATTACKING:
               // Debug.Log("Case: Attacking");
                HeroIsAttacking();
                break;

            case CharacterState.DEAD:
                HeroIsDead();
                break;
        }

        //-> Update Hero animation
        heroAnimator.SetInteger("heroState", (int)HeroState);



    }

    private void HeroIsDead()
    {
        AliveHerosList.Remove(this.gameObject);
        Destroy(this.gameObject, 0.30f);
        numOfAliveHeros--;
    }

    private void HeroIsAttacking()
    {

        switch (Target)
        {
            case TargetType.CastleTarget:

                if (EnemyCastleScript.castleHealth > 0)
                {
                    EnemyCastleScript.castleHealth -= attackPower * Time.deltaTime;
                    EnemyCastleScript.ActivateParticles(true);
                    EnemyCastleScript.PlayParticles();
                }
                else
                {
                    HeroState = CharacterState.IDLE;
                    EnemyCastleScript.PauseParticles();
                }

                break;

            case TargetType.OrcTarget:

                if (targetScript.orcHealth > 0)
                    targetScript.orcHealth -= attackPower * Time.deltaTime;
                else
                {
                    // target is dead, move to next one
                    if (reachedTheCastle)
                    {
                        Target = TargetType.CastleTarget;
                        HeroState = CharacterState.ATTACKING;
                       
                    }else
                         HeroState = CharacterState.MOVING;
                }
                //else: move to next target
                break;
        }
    }

    private void HeroIsMoving()
    {
        if (ORCsScript.numOfAliveORCs > 0)
        {
            //move to their location
            orcTarget = ORCsScript.AliveORCsList[0];
            //targetScript = orcTarget.GetComponent<ORCsScript>();
            Vector2 targetPosition = new Vector2(orcTarget.transform.position.x-2, orcTarget.transform.position.y-0.8f);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            //move to enemy castle
            if (enemyCastle != null)
                transform.position = Vector2.MoveTowards(transform.position, enemyCastlePosition, moveSpeed * Time.deltaTime);
        }
    }

    private void HeroIsIdle()
    {
        //TODO Idle
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "EnemyCastle")
        {
            Target = TargetType.CastleTarget;
            HeroState = CharacterState.ATTACKING;
            reachedTheCastle = true;
        }
        else if (collision.tag == "ORC_TAG")
        {
            Target = TargetType.OrcTarget;
            HeroState = CharacterState.ATTACKING;
            targetScript = collision.gameObject.GetComponent<ORCsScript>();
        }

    }


}

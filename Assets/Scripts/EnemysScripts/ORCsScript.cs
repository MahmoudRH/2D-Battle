using System.Collections.Generic;
using UnityEngine;

public class ORCsScript : MonoBehaviour
{
    public static List<GameObject> AliveORCsList = new List<GameObject>();

    //ORC properities: 
    public float attackPower, orcHealth, moveSpeed;
    public int killingReward;
    TargetType Target;
    CharacterState OrcState;

    [SerializeField]
    Transform herosCastle;
    [SerializeField]
    Animator orcAnimator;

    GameObject heroTarget;
    HerosScript targetScript;
    public static int killes = 0;

    public AudioSource deathSource;

    private bool reachedTheCastle = false;
    bool canPlay = true;

    private void Awake()
    {
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
        orcAnimator.SetInteger("orcState", (int) OrcState);
    }

    private void OrcIsDead()
    {
        if (canPlay)
        {
            killes++;
            deathSource.PlayOneShot(deathSource.clip);
            canPlay = false;
            Ui_events.totalBalance += killingReward;
        }
        AliveORCsList.Remove(this.gameObject);
        Destroy(this.gameObject, 1f);
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

                if (targetScript.heroHealth > 0)
                    targetScript.heroHealth -= attackPower * Time.deltaTime;
                else
                {
                    // target is dead, move to next one
                    if (reachedTheCastle)
                    {
                        Target = TargetType.CastleTarget;
                        OrcState = CharacterState.ATTACKING;

                    }
                    else
                        OrcState = CharacterState.MOVING;
                }
                break;
        }
    }

    private void OrcIsMoving()
    {
        if (HerosScript.AliveHerosList.Count > 0)
        {
            //move to their location
            heroTarget = HerosScript.AliveHerosList[0];
            Vector2 targetPosition = new Vector2(heroTarget.transform.position.x + 1.1f, heroTarget.transform.position.y + 1.1f);
            transform.position = Vector2.MoveTowards(transform.position, heroTarget.transform.position, moveSpeed * Time.deltaTime);
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
            reachedTheCastle = true;
        }
        else if (collision.tag == "HERO_TAG")
        {
            Target = TargetType.HeroTarget;
            OrcState = CharacterState.ATTACKING;
            targetScript = collision.gameObject.GetComponent<HerosScript>();
        }


    }

}

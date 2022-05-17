using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyCastleScript : MonoBehaviour
{
    public static float castleHealth = 500;
    [SerializeField]
    Slider castleHealthBar;
    [SerializeField]
    Text healthBarText;
    public AudioSource explosionSound;
    public AudioSource brokenSound;
    bool canPlay = true;


    public ParticleSystem breakCastlePartcicles, somkCastlePartcicles;

    public float timerInSeconds = 1;
    public int instantiationDelay = 4;

    int currentSortingOrder = 1;

    //prefabs
    [SerializeField]
    GameObject Orc1, Orc2, Orc3;


    private const int ORC1 = 1, ORC2 = 2, ORC3 = 3;


    [SerializeField]
    SpriteRenderer forground;
    [SerializeField]
    Sprite halfDestroyed, veryDestroyed;

    /*
     * level1 Variation:
     *  9 -> ORC1
     *  5 -> ORC2
     *  3 -> ORC3
     */

    private int[] level1Variation = {ORC1, ORC1, ORC1, ORC1, ORC1, ORC1, ORC1, ORC1, ORC1,
                                     ORC2, ORC2, ORC2, ORC2, ORC2,
                                     ORC3, ORC3, ORC3 };

    Vector3 spawnLocation;
    Quaternion OrcsRotatoin;
    private float castleXPosition, castleYPosition;

    void Start()
    {
        somkCastlePartcicles.Pause();
        breakCastlePartcicles.Pause();
        castleXPosition = this.gameObject.transform.position.x;
        castleYPosition = this.gameObject.transform.position.y;
        // spawnLocation = new Vector3(castleXPosition -2  , Random.Range(castleYPosition-5,castleYPosition+5),0);
        OrcsRotatoin = new Quaternion(0, 180, 0, 1);
    }


    void Update()
    {

        IncreaseTheTimer();

        if (Mathf.FloorToInt(timerInSeconds) % instantiationDelay == 0)
        {
            GameObject orc = InstantiateOrc();
            ResetTheTimer();
            instantiationDelay++;
        }
        if(instantiationDelay > 15)
        {
            instantiationDelay = 5;
        }


        if (castleHealth > 0)
        {
            UpdateHealthUI();
        }
        else
        {
            canPlay = true;
            if (canPlay)
            {
                explosionSound.PlayOneShot(explosionSound.clip);
                canPlay = false;
            }
            Destroy(this.gameObject);
        }

    }

    private void UpdateHealthUI()
    {
        castleHealthBar.value = castleHealth / 500f;
        healthBarText.text = Mathf.Floor(castleHealth / 5f) + " %";
        

        if (castleHealth / 5 <= 70)
        {
            if (canPlay)
            {
                explosionSound.PlayOneShot(explosionSound.clip);
                canPlay = false;
            }
            somkCastlePartcicles.Play();
            forground.sprite = halfDestroyed;
        }
        if (castleHealth / 5 <= 20)
        {
            forground.sprite = veryDestroyed;
        }
    }

    private void IncreaseTheTimer()
    {
        timerInSeconds += Time.deltaTime;
    }

    private void ResetTheTimer()
    {
        timerInSeconds = 1;
    }

    private GameObject InstantiateOrc()
    {
        GameObject orcInstance = null;

        int randomOrcType = Random.Range(0, level1Variation.Length - 1);
        int orcType = level1Variation[randomOrcType];
        // Debug.Log("InstantiateOrc, orcType: " + orcType);
        spawnLocation = new Vector3(castleXPosition - 2, Random.Range(castleYPosition - 1, castleYPosition + 2), 0);
        switch (orcType)
        {
            case ORC1:
                orcInstance = Instantiate(Orc1, spawnLocation, OrcsRotatoin);
                break;
            case ORC2:
                orcInstance = Instantiate(Orc2, spawnLocation, OrcsRotatoin);
                break;
            case ORC3:
                orcInstance = Instantiate(Orc3, spawnLocation, OrcsRotatoin);
                break;
        }
        orcInstance.GetComponent<SpriteRenderer>().sortingOrder = currentSortingOrder;
        currentSortingOrder++;

        return orcInstance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HERO_TAG")
        {
            brokenSound.PlayOneShot(brokenSound.clip);
        }
    }

   

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "HERO_TAG")
        {
            breakCastlePartcicles.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HERO_TAG")
        {
            breakCastlePartcicles.Pause();
            brokenSound.Pause();
        }
    }
}

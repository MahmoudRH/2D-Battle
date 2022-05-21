using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HerosCastleScript : MonoBehaviour
{
    //public static float originalCastleHealth = 500;
    public static float castleHealth = 500;
    [SerializeField]
    Slider castleHealthBar;
    [SerializeField]
    Text healthBarText;
    public AudioSource explosionSound;
    public AudioSource brokenSound;
    bool canPlay = true;
    public ParticleSystem breakCastlePartcicles, somkCastlePartcicles;
    [SerializeField]
    SpriteRenderer forground;
    [SerializeField]
    Sprite halfDestroyed, veryDestroyed;
    public static bool isLose = false;

    public static List<GameObject> attackingOrcs = new List<GameObject>();

    void Start()
    {
        somkCastlePartcicles.Pause();
        breakCastlePartcicles.Pause();
        castleHealthBar.value = 1f;
        healthBarText.text = "100 %";
    }


    void Update()
    {
        if (castleHealth > 0)
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
        else
        {
            if (canPlay)
            {
                explosionSound.PlayOneShot(explosionSound.clip);
                canPlay = false;
            }
            isLose = true;
            Destroy(this.gameObject);
        }

        if (attackingOrcs.Count == 0)
        {

             breakCastlePartcicles.Pause();
             breakCastlePartcicles.gameObject.SetActive(false);
             brokenSound.Pause();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ORC_TAG")
        {
            breakCastlePartcicles.gameObject.SetActive(true);
            breakCastlePartcicles.Play();
            brokenSound.Play();
            attackingOrcs.Add(collision.gameObject);
        }
    }
}

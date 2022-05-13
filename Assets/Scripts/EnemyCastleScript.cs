using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyCastleScript : MonoBehaviour
{
    //public static float originalCastleHealth = 500;
    public static float castleHealth = 500;
    [SerializeField]
    Slider castleHealthBar;
    [SerializeField]
    Text healthBarText;

    void Start()
    {
        castleHealthBar.value = castleHealth/500f;
        healthBarText.text = Mathf.Floor(castleHealth/5f)+" %";
    }


    void Update()
    {
        if (castleHealth > 0)
        {
            castleHealthBar.value = castleHealth / 500f;
            healthBarText.text = Mathf.Floor(castleHealth / 5f) + " %";
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
}

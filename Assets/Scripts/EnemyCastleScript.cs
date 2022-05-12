using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyCastleScript : MonoBehaviour
{
    public static float castleHealth = 1000;
    [SerializeField]
    Slider castleHealthBar;
    [SerializeField]
    Text healthBarText;
    // Start is called before the first frame update
    void Start()
    {
        castleHealthBar.value = castleHealth/1000f;
        healthBarText.text = Mathf.Floor(castleHealth/10f)+" %";
    }

    // Update is called once per frame
    void Update()
    {
        castleHealthBar.value = castleHealth / 1000f;
        healthBarText.text = Mathf.Floor(castleHealth/10f)+" %";
    }
}
